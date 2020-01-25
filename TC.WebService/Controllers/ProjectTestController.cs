using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TC.DataAccess;
using TC.DataAccess.Repositories;
using TC.Entity.Entities;
using TC.WebService.Helpers;
using TC.WebService.ViewModels;

namespace TC.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ProjectTestController : AuthBaseController
    {
        private IProjectRepository _projectRepository;
        private TestInfoRepository _testInfoRepository;
        private IUnitOfWork _unitOfWork;

        public ProjectTestController(TestInfoRepository testInfoRepository, IProjectRepository projectRepository, IUnitOfWork unitOfWork, IUserHelper userHelper)
            : base(userHelper)
        {
            _projectRepository = projectRepository;
            _testInfoRepository = testInfoRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{projectId}")]
        public async Task<List<TestInfoViewModel>> Get(int projectId)
        {
            var testInfos = await _testInfoRepository.GetTestInfos(projectId);
            return testInfos.Select(x => new TestInfoViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                ProjectId= x.ProjectId,
                Description = x.Description,
                Commands = x.SeleniumCommands
            }).ToList();
        }

        [HttpGet]
        public async Task<List<TestInfoViewModel>> Get()
        {
            var user = GetUser();
            var projects = _projectRepository.GetProjectsByUser(user.Guid.ToString());
            var testInfos = await _testInfoRepository.GetUsersTestInfo(projects.Select(x => x.Id).ToList());

            return testInfos.Select(x => new TestInfoViewModel()
            {
                Id = x.Id,
                ProjectId = x.ProjectId,
                Name = x.Name,
                Description = x.Description,
                Commands = x.SeleniumCommands
            }).ToList();
        }


        [HttpPost]
        public IActionResult Post(ProjectTestViewModel viewModel)
        {
            var user = GetUser();
            var project = _projectRepository.GetProjectByUser(GetUserGuid(), viewModel.ProjectId);
            if (project == null)
            {
                return BadRequest();
            }
            var testInfo = new TestInfo()
            {
                ProjectId = viewModel.ProjectId,
                SeleniumCommands = viewModel.SeleniumCommands,
                Description=viewModel.Description,
                Name=viewModel.Name
            };
            project.TestInfos.Add(testInfo);
            _unitOfWork.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = testInfo.Id });
        }
    }
}