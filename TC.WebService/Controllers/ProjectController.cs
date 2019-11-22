using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ProjectController : ControllerBase
    {
        private ProjectRepository _projectRepository;
        private UserRepository _userRepository;
        private IUnitOfWork _unitOfWork;

        public ProjectController(ProjectRepository projectRepository, UserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _projectRepository = projectRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<List<ProjectViewModel>> Get()
        {
            string guid = UserHelper.GetGuid(User);

            return _projectRepository.GetProjectsByUser(guid).Select(x => GetProjectViewModel(x)).ToList();
        }
        [HttpGet("{id}")]
        public async Task<ProjectViewModel> Get(int id)
        {
            string guid = UserHelper.GetGuid(User);

            var project = _projectRepository.GetProjectByUser(guid, id);
            return GetProjectViewModel(project);

        }

        [HttpPost]
        public IActionResult Post(ProjectCreateViewModel viewModel)
        {
            string guid = UserHelper.GetGuid(User);
            var currnetUser = _userRepository.GetByGuid(guid);

            var domains = new List<ProjectDomain>();
            foreach (var domain in viewModel.Domains.Split(","))
            {
                Uri myUri = null;
                try
                {
                    myUri = new Uri(domain);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
                var host = myUri.Host.Replace("www.", "");
                domains.Add(new ProjectDomain
                {
                    Domain = host
                });

            }
            var users = new List<UserInProject>();
            foreach (var email in viewModel.UsersEmail.Split(","))
            {
                if (string.IsNullOrEmpty(email))
                {
                    continue;
                }
                UserModel user = _userRepository.GetByEmail(email);
                if (user == null)
                {
                    var password = System.Guid.NewGuid().ToString();
                    //TODO send email with activation link and password
                    _userRepository.Create(new UserModel
                    {
                        Email = email,
                        Password = UserHelper.PasswordHash(password),
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

            }
            // check is current user is in list if not added
            if (users.FirstOrDefault(x => x.Id == currnetUser.Id) == null)
            {
                users.Add(new UserInProject
                {
                    UserModelId = currnetUser.Id
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
        private ProjectViewModel GetProjectViewModel(Project project)
        {
            return new ProjectViewModel()
            {
                Id = project.Id,
                Name = project.Name,
                ProjectDomain = project.ProjectDomains.Select(x => new ProjectDomainViewModel { Domain = x.Domain }).ToList()
            };
        }
    }
}