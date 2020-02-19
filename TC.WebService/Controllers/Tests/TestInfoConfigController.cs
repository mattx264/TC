using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TC.DataAccess.Repositories;
using TC.DataAccess.Repositories.Interfaces;
using TC.WebService.ViewModels;
using TC.WebService.ViewModels.Projects;

namespace TC.WebService.Controllers.Test
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestInfoConfigController : ControllerBase
    {

        #region Constructor
        private ITestInfoConfigRepository _testInfoConfigRepository;
        private IConfigProjectTestRepository _configProjectTestRepository;
        private IProjectTestConfigRepository _projectTestConfigRepository;
        private ITestInfoRepository _testInfoRepository;

        public TestInfoConfigController(
            ITestInfoConfigRepository testInfoConfigRepository,
            IConfigProjectTestRepository configProjectTestRepository,
            IProjectTestConfigRepository projectTestConfigRepository,
            ITestInfoRepository testInfoRepository
            )
        {
            _testInfoConfigRepository = testInfoConfigRepository;
            _configProjectTestRepository = configProjectTestRepository;
            _projectTestConfigRepository = projectTestConfigRepository;
            _testInfoRepository = testInfoRepository;
        }
        #endregion
        #region GET
        [HttpGet("{testId}")]
        public IActionResult Get(int testId)
        {
            var configs = _configProjectTestRepository.FindAll().ToList();
            var testInfoConfig=_testInfoConfigRepository.FindByTestId(testId);
            if (testInfoConfig != null && testInfoConfig.Count >0)
            {
                var testInfoConfigViewMode = testInfoConfig.Select(x=>new ProjectTestConfigViewModel().Convert(x));
                return Ok(testInfoConfigViewMode);
            }
            var projectId = _testInfoRepository.FindById(testId).ProjectId;
            var projectTestConfigs = _projectTestConfigRepository.GetByProjectId(projectId);
            if (projectTestConfigs != null && projectTestConfigs.Count > 0)
            {
                var configProjectTestViewModel = projectTestConfigs.Select(x=>new ProjectTestConfigViewModel().Convert(x));
                return Ok(configProjectTestViewModel);
            }
            return Ok(configs.Select(x => new ProjectTestConfigViewModel().Convert(0, projectId, x)).ToList());
        }
        #endregion
    }
}