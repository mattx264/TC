using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TC.DataAccess.Repositories;
using TC.WebService.ViewModels;

namespace TC.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestInfoController : ControllerBase
    {
        private TestInfoRepository _testInfoRepository;

        public TestInfoController(TestInfoRepository testInfoRepository)
        {
            _testInfoRepository = testInfoRepository;
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
           var testInfo= _testInfoRepository.FindById(id);
            return Ok(new TestInfoViewModel()
            {
                Commands = testInfo.SeleniumCommands,
                Description = testInfo.Description,
                Id = testInfo.Id,
                Name = testInfo.Name,
                ProjectId = testInfo.ProjectId
            });
        }
    }
}