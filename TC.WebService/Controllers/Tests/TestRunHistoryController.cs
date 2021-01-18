 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TC.DataAccess.Repositories;
using TC.DataAccess.Repositories.Interfaces;
using TC.WebService.Extensions;
using TC.WebService.ViewModels.Tests;

namespace TC.WebService.Controllers.Tests
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TestRunHistoryController : AuthBaseController
    {
        private ITestRunHistoryRepository _testRunHistoryRepository;

        public TestRunHistoryController(IUserHelper userHelper,ITestRunHistoryRepository testRunHistoryRepository) : base(userHelper)
        {
            _testRunHistoryRepository = testRunHistoryRepository;
        }
        [HttpGet("{testInfoId}")]
        public IActionResult Get(int testInfoId)
        {
            if(testInfoId == 0)
            {
                return BadRequest();
            }
            var result = _testRunHistoryRepository.FindAll().Where(x => x.TestInfoId == testInfoId).Select(x => new TestRunHistoryViewModel(x)); ;
            return Ok(result);
        }
    }
}