using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TC.DataAccess;
using TC.DataAccess.Repositories;
using TC.Entity.Entities;
using TC.WebService.Helpers;
using TC.WebService.ViewModels;
using TC.Entity.Entities.Projects;
namespace TC.WebService.Controllers.Projects
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ProjectController : AuthBaseController
    {
        private IProjectRepository _projectRepository;
        private IUserRepository _userRepository;
        private IUtilHelper _utilHelper;
        private IUnitOfWork _unitOfWork;

        public ProjectController(
            IProjectRepository projectRepository,
            IUserHelper userHelper,
            IUnitOfWork unitOfWork,
            IUserRepository userRepository,
            IUtilHelper utilHelper)
            : base(userHelper)
        {
            _projectRepository = projectRepository;
            _userRepository = userRepository;
            _utilHelper = utilHelper;
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<List<ProjectViewModel>> Get()
        {
            string guid = GetUserGuid();

            return _projectRepository.GetProjectsByUser(guid).Select(x => GetProjectViewModel(x)).ToList();
        }
        [HttpGet("domain/{domain}")]
        public async Task<ProjectViewModel> Get(string domain)
        {
            string guid = GetUserGuid();
            var project = _projectRepository.GetProjectByDomain(guid, domain);
            return project == null ? null : GetProjectViewModel(project);
        }
        [HttpGet("{id}")]
        public async Task<ProjectViewModel> Get(int id)
        {
            string guid = GetUserGuid();

            var project = _projectRepository.GetProjectByUser(guid, id);
            return GetProjectViewModel(project);

        }

        [HttpPost]
        public IActionResult Post(ProjectCreateViewModel viewModel)
        {
            string guid = GetUserGuid();
            var currnetUser = _userRepository.GetByGuid(guid);
            var users = new List<UserInProject>();

            #region Domain Information
            var domains = new List<ProjectDomain>();

            foreach (var domain in viewModel.Domains.Split(","))
            {
                try
                {
                    var host = _utilHelper.GetDomain(domain.Trim());
                    if (host == null)
                        return BadRequest($"Missing or Incorrect Domain Name: {domain.Trim()}");

                    domains.Add(new ProjectDomain
                    {
                        Domain = host
                    });
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }
            #endregion


            #region User Email
            foreach (var email in viewModel.UsersEmail)
            {
                if (string.IsNullOrEmpty(email))
                {
                    continue;
                }

                var user = _userRepository.GetByEmail(email);
                if (user == null)
                {
                    var password = Guid.NewGuid().ToString();
                    //TODO send email with activation link and password
                    _userRepository.Create(new UserModel
                    {
                        Email = email,
                        Password = _userHelper.PasswordHash(password),
                        Guid = System.Guid.NewGuid(),
                        Name = viewModel.Name,
                    });
                    
                    _unitOfWork.SaveChanges();
                    
                    user = _userRepository.GetByEmail(email);
                }

                if(users.FirstOrDefault(x => x.UserModelId == user.Id) == null)
                {
                    users.Add(new UserInProject
                    {
                        UserModelId = user.Id,
                        UserProjectStatusId = 1
                    });
                }   
            }

            // check is current user is in list if not added
            if (users.FirstOrDefault(x => x.UserModelId == currnetUser.Id) == null)
            {
                users.Add(new UserInProject
                {
                    UserModelId = currnetUser.Id,
                    UserProjectStatusId = 2
                });
            }
            #endregion

            _projectRepository.Create(new Entity.Entities.Projects.Project()
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
                ProjectDomains = domains,
                UserInProject = users
            });

            _unitOfWork.SaveChanges();
            return Ok();
        }




        [HttpPut]
        public IActionResult Put(ProjectCreateViewModel viewModel)
        {
            #region User Information
            var currnetUser = GetUser();
            if (viewModel.Id == null)
            {
                return BadRequest("Project id has to be set");
            }
            var project = _projectRepository.FindById(viewModel.Id.Value);
            
            var userInProject = project.UserInProject.FirstOrDefault(x => x.UserModelId == currnetUser.Id);
            if (userInProject == null)
            {
                return BadRequest("User is not part of project");
            }
            var users = new List<UserInProject>() { userInProject };
            #endregion

            #region Domain Information
            var proposedDomains = new List<string>(viewModel.Domains.Split(","));

            try
            {
                // Remove existing domains
                project.ProjectDomains.Where(x => x.IsActive).ToList().ForEach(existingDomain =>
                {
                    if (proposedDomains.FirstOrDefault(
                        pd => _utilHelper.GetDomain(pd.Trim())
                                        .Equals(existingDomain.Domain.Trim(), 
                                                StringComparison.OrdinalIgnoreCase)) == null)
                    {
                        existingDomain.IsActive = false;
                        existingDomain.DateModified = DateTime.Now;
                        existingDomain.ModifiedBy = currnetUser.Email;
                    }
                });

                // Adding new domains
                proposedDomains.ForEach(x =>
                {
                    var host = _utilHelper.GetDomain(x.Trim());

                    if (project.ProjectDomains.FirstOrDefault(x => x.Domain == host) == null)
                    {
                        project.ProjectDomains.Add(new ProjectDomain
                        {
                            Domain = host
                        });
                    }
            });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            #endregion

            #region User Emails
            // Remove users no longer part of project
            project.UserInProject.Where(x => x.IsActive).ToList().ForEach(existingUsers =>
            {
                if (viewModel.UsersEmail.FirstOrDefault(
                        userEmail => userEmail.Equals(existingUsers.UserModel.Email,
                                                StringComparison.OrdinalIgnoreCase)) == null)
                {
                    existingUsers.IsActive = false;
                    existingUsers.DateModified = DateTime.Now;
                    existingUsers.ModifiedBy = currnetUser.Email;
                }
            });


            // Add new users
            viewModel.UsersEmail
                .Where(x => !string.IsNullOrEmpty(x))
                .ToList()
                .ForEach(email =>
                {
                    var user = _userRepository.GetByEmail(email);

                    // Add new users
                    if (user == null)
                    {
                        var password = System.Guid.NewGuid().ToString();

                        //TODO send email with activation link and password
                        _userRepository.Create(new UserModel
                        {
                            Email = email,
                            Password = _userHelper.PasswordHash(password),
                            Guid = System.Guid.NewGuid(),
                            Name = viewModel.Name,
                        });

                        user = _userRepository.GetByEmail(email);
                    }

                    var usersInProject = project.UserInProject.Where(x => x.UserModel.Email.Equals(user.Email, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

                    if (usersInProject == null)
                    {
                        project.UserInProject.Add(new UserInProject
                        {
                            UserModelId = user.Id
                        });
                    } 
                    else if (usersInProject.IsActive == false)
                    {
                        usersInProject.IsActive = true;
                        usersInProject.DateModified = DateTime.Now;
                        usersInProject.ModifiedBy = currnetUser.Email;
                    }
                });
            #endregion

            project.Name = viewModel.Name;
            project.Description = viewModel.Description;


            _projectRepository.Update(project);
            _unitOfWork.SaveChanges();

            return Ok();
        }



        private ProjectViewModel GetProjectViewModel(Entity.Entities.Projects.Project project)
        {
            return new ProjectViewModel()
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                ProjectDomain = project.ProjectDomains == null ? null : project.ProjectDomains.Where(x => x.IsActive).Select(x => new ProjectDomainViewModel { Domain = x.Domain }).ToList(),
                UserInProject = project.UserInProject == null ? null : project.UserInProject.Where(x => x.IsActive).Select(x => new UserInProjectViewModel { UserEmail = x.UserModel.Email, Status = x.UserProjectStatus.Name }).ToList()
            };
        }

        private ProjectDetailsViewModel GetProjectDetailsViewModel(Entity.Entities.Projects.Project project)
        {
            return new ProjectDetailsViewModel()
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                ProjectDomain = project.ProjectDomains == null ? null : project.ProjectDomains.Where(x => x.IsActive).Select(x => new ProjectDomainViewModel { Domain = x.Domain }).ToList(),
                UserInProject = project.UserInProject == null ? null : project.UserInProject.Where(x => x.IsActive).Select(x => new UserInProjectViewModel { UserEmail = x.UserModel.Email, Status = x.UserProjectStatus.Name }).ToList(),
                DateModified = project.DateModified,
                ModifiedBy = project.ModifiedBy,
                LastTestRunDate = project.TestInfos.Where(x => x.IsActive).OrderByDescending(x => x.DateModified)
                    .Select(x => x.DateAdded)
                    .DefaultIfEmpty(Convert.ToDateTime("1/1/1900"))
                    .FirstOrDefault()
            };
        }

        private string GetDomain(string domainInput)
        {

            string domain = new Regex(@"^(?:https?:\/\/)?(?:[^@\/\n]+@)?(?:www\.)?([^:\/?\n]+)(\.[a-z]+)").Match(domainInput).Value;

            return string.IsNullOrEmpty(domain) ? null : domain;

        }

        [HttpPost]
        [Route("deleteProject")]
        public IActionResult DeleteProject(int[] projectId)
        {
            var user = GetUser();

            projectId.AsEnumerable()
            .Select(x => _projectRepository.FindById(x)).ToList()
            .ForEach(x =>
            {
                //_projectRepository.Delete(x);
                x.IsActive = false;
                x.ModifiedBy = user.Name;
                x.DateModified = DateTime.Now;
            });

            _unitOfWork.SaveChanges();
            return Ok();
        }

        [HttpGet]
        [Route("getSeleniumCommands")]
        public IActionResult GetSeleniumCommands(int projectId)
        {
            var user = GetUser();
            var result = _projectRepository
                .GetProjectByUser(user.Guid.ToString(), projectId)
                .TestInfos
                .Where(x => x.ProjectId == projectId);

            return Ok(result);
        }

        [HttpGet]
        [Route("getProjects")]
        public IActionResult GetProjects()
        {
            string guid = GetUserGuid();
            return Ok(_projectRepository.GetProjectsByUser(guid).Select(x => GetProjectViewModel(x)));
        }

        [HttpGet]
        [Route("getProjectDetails")]
        public IActionResult GetProjectDetails(int id)
        {
            string guid = GetUserGuid();
            return Ok(_projectRepository.GetProjectsByUser(guid).Select(x => GetProjectDetailsViewModel(x)).ToList());
        }
    }
}