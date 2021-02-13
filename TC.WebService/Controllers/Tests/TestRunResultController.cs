using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TC.DataAccess.Repositories;
using TC.DataAccess.Repositories.Interfaces;
using TC.WebService.ViewModels.Tests;

namespace TC.WebService.Controllers.Tests
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestRunResultController : ControllerBase
    {
        private ITestRunResultRepository _testRunResultRepository;
        private ITestRunHistoryRepository _testRunHistoryRepository;

        public TestRunResultController(ITestRunResultRepository testRunResultRepository, ITestRunHistoryRepository testRunHistoryRepository)
        {
            _testRunResultRepository = testRunResultRepository;
            _testRunHistoryRepository = testRunHistoryRepository;
        }
        [HttpGet("{testHistoryId}")]
        public IActionResult Get(int testHistoryId)
        {
            var testRunResults = _testRunResultRepository.GetByTestHistoryId(testHistoryId);
            if (testRunResults.Count == 0)
            {
                return Ok();
            }
            var testInfo = _testRunHistoryRepository.FindById(testRunResults.FirstOrDefault().TestRunHistoryId).TestInfo;
            var result = testRunResults.Select(x => new TestRunResultViewModel(x, testInfo));

            return Ok(result);
        }
    }
}