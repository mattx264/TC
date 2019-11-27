using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TC.DataAccess.Repositories;
using TC.WebService.Helpers;
using TC.WebService.ViewModels;

namespace TC.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ProjectTestController : ControllerBase
    {
        private TestInfoRepository _testInfoRepository;

        public ProjectTestController(TestInfoRepository testInfoRepository)
        {
            _testInfoRepository = testInfoRepository;
        }
        [HttpGet("{projectId}")]
        public async Task<List<TestInfoViewModel>> Get(int projectId)
        {
            string guid = UserHelper.GetGuid(User);

            var testInfos = await _testInfoRepository.GetTestInfos(projectId);
            return testInfos.Select(x => new TestInfoViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Commands = x.SeleniumCommands
            }).ToList();
        }
        [HttpPost]
        public IActionResult Post(ProjectTestViewModel viewModel)
        {
            return Ok();
        }
    }
}