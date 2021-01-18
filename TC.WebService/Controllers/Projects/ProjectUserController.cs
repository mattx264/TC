using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TC.DataAccess;
using TC.DataAccess.Repositories;
using TC.Entity.Entities;
using TC.WebService.Extensions;
using TC.WebService.ViewModels;

namespace TC.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectUserController : AuthBaseController
    {
        private IProjectRepository _projectRepository;
        private IUserRepository _userRepository;
        private IUnitOfWork _unitOfWork;

        public ProjectUserController(IProjectRepository projectRepository, IUserRepository userRepository, IUnitOfWork unitOfWork, IUserHelper userHelper)
            : base(userHelper)
        {
            _projectRepository = projectRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;

        }
        [HttpPost]
        public IActionResult Post(ProjectUserViewModel viewModel)
        {
            var user = GetUser();
            var project = _projectRepository.FindById(viewModel.ProjectId);
            if (project == null)
            {
                return BadRequest();
            }
            if (project.UserInProject.Any(x => x.UserModelId == user.Id))
            {
                return Ok("User exist in project");
            }
            project.UserInProject.Add(new UserInProject
            {
                UserModelId = user.Id
            });
            _unitOfWork.SaveChanges();
            return Ok();
        }
    }
}