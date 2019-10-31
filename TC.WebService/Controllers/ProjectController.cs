using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TC.DataAccess.Repositories;
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

        public ProjectController(ProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        [HttpGet]
        public async Task<List<ProjectViewModel>> Get()
        {
            string guid= UserHelper.GetGuid(User);
           
            return _projectRepository.GetProjectByUser(guid).Select(x => new ProjectViewModel() {
                Id=x.Id,
                Name=x.Name
        }).ToList();
        }
    }
}