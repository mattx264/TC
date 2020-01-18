using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TC.DataAccess;
using TC.DataAccess.Repositories;
using TC.Entity.Entities;
using TC.WebService.Helpers;
using TC.WebService.ViewModels;

namespace TC.WebService.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ProjectController : AuthBaseController
    {
        private IProjectRepository _projectRepository;
        private IUserRepository _userRepository;
        private IUnitOfWork _unitOfWork;

        public ProjectController(IProjectRepository projectRepository, IUserHelper userHelper, IUnitOfWork unitOfWork, IUserRepository userRepository)
            : base(userHelper)
        {
            _projectRepository = projectRepository;
            _userRepository = userRepository;
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

            var currnetUser = GetUser();

            var domains = new List<ProjectDomain>();
            foreach (var domain in viewModel.Domains.Split(","))
            {
                try
                {
                    string host = GetDomain(domain);
                    if (host == null)
                    {
                        return BadRequest($"Missing or Incorrect Domain Name: { domain}");
                    }
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
            var users = new List<UserInProject>();
            
            foreach (var email in viewModel.UsersEmail)
            {
                if (string.IsNullOrEmpty(email))
                {
                    continue;
                }
                UserModel user = _userRepository.GetByEmail(email);
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
                users.Add(new UserInProject
                {
                    UserModelId = user.Id,
                    UserProjectStatusId = 1
                });

            }

            // check is current user is in list if not added
            if (users.FirstOrDefault(x => x.Id == currnetUser.Id) == null)
            {
                users.Add(new UserInProject
                {
                    UserModelId = currnetUser.Id,
                    UserProjectStatusId=2
                });
            }

            _projectRepository.Create(new Project()
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

            var domainsToCheck = new List<string>();
            foreach (var domain in viewModel.Domains.Split(","))
            {
                try
                {
                    string host = GetDomain(domain);
                    domainsToCheck.Add(host);
                    if (project.ProjectDomains.FirstOrDefault(x => x.Domain == host) == null)
                    {
                        project.ProjectDomains.Add(new ProjectDomain
                        {
                            Domain = host
                        });
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            var users = new List<UserInProject>();

            viewModel.UsersEmail
                .Where(x => string.IsNullOrEmpty(x))
                .ToList()
                .ForEach(email =>
                {
                    var user = _userRepository.GetByEmail(email);

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

                        _unitOfWork.SaveChanges();
                        user = _userRepository.GetByEmail(email);
                    }

                    users.Add(new UserInProject
                    {
                        UserModelId = user.Id
                    });
                });

            // check is current user is in list if not added
            if (users.FirstOrDefault(x => x.Id == currnetUser.Id) == null)
            {
                users.Add(new UserInProject
                {
                    UserModelId = currnetUser.Id
                });
            }

            _unitOfWork.SaveChanges();

            return Ok();
        }
        private ProjectViewModel GetProjectViewModel(Project project)
        {
            return new ProjectViewModel()
            {
                Id = project.Id,
                Name = project.Name,
                ProjectDomain = project.ProjectDomains == null ? null : project.ProjectDomains.Select(x => new ProjectDomainViewModel { Domain = x.Domain }).ToList(),
                UserInProject = project.UserInProject == null ? null : project.UserInProject.Select(x => new UserInProjectViewModel { UserEmail = x.UserModel.Email, Status = x.UserProjectStatus.Name }).ToList()
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
            return Ok(_projectRepository.GetProjectsByUser(guid));
        }
    }
}