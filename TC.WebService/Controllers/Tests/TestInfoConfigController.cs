using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TC.DataAccess;
using TC.DataAccess.Repositories.Interfaces;
using TC.Entity.Entities;
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
        private IUnitOfWork _unitOfWork;

        public TestInfoConfigController(
            ITestInfoConfigRepository testInfoConfigRepository,
            IConfigProjectTestRepository configProjectTestRepository,
            IProjectTestConfigRepository projectTestConfigRepository,
            ITestInfoRepository testInfoRepository,
            IUnitOfWork unitOfWork
            )
        {
            _testInfoConfigRepository = testInfoConfigRepository;
            _configProjectTestRepository = configProjectTestRepository;
            _projectTestConfigRepository = projectTestConfigRepository;
            _testInfoRepository = testInfoRepository;
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region GET
        [HttpGet("{testId}")]
        public IActionResult Get(int testId)
        {
            if(testId == 0)
            {
                throw new ArgumentNullException(nameof(testId));
            }

            var configs = _configProjectTestRepository.FindAll().ToList();
            var testInfoConfig = _testInfoConfigRepository.FindByTestId(testId);
            if (testInfoConfig != null && testInfoConfig.Count > 0)
            {
                var testInfoConfigViewMode = testInfoConfig.Select(x => new TestInfoConfigViewModel(x));
                return Ok(testInfoConfigViewMode);
            }
            var projectId = _testInfoRepository.FindById(testId).ProjectId;
            var projectTestConfigs = _projectTestConfigRepository.GetByProjectId(projectId);
            if (projectTestConfigs != null && projectTestConfigs.Count > 0)
            {
                var configProjectTestViewModel = projectTestConfigs.Select(x => new ProjectTestConfigViewModel().Convert(x));
                return Ok(configProjectTestViewModel);
            }
            return Ok();
        }
        #endregion

        #region POST
        [HttpPost]
        public IActionResult Post(IList<ProjectTestConfigViewModel> viewModel)
        {
            if (viewModel is null)
            {
                throw new ArgumentNullException(nameof(viewModel));
            }

            foreach (var config in viewModel)
            {
                var configProjectTest = _configProjectTestRepository.FindById(config.ConfigProjectTestId);
                if (configProjectTest == null)
                {
                    return BadRequest("Config project cannot be null");
                }
                //validate if valus is correct with type
                switch (configProjectTest.Type)
                {
                    case ConfigProjectTest.ConfigProjectTestEnum.Boolean:

                        if (config.Value == "false" || config.Value == "true")
                        {
                            break;
                        }
                        return BadRequest("Value boolean is not valid");
                    case ConfigProjectTest.ConfigProjectTestEnum.List:
                        // OPEN QUESTION
                        break;
                    case ConfigProjectTest.ConfigProjectTestEnum.String:
                        // String cannot be invalid
                        break;

                }
                if (config.Id == 0)
                {
                    _testInfoConfigRepository.Create(new TestInfoConfig()
                    {
                        ConfigProjectTestId = config.ConfigProjectTestId,
                        TestInfoId = config.Id,
                        Value = config.Value
                    });
                }
                else
                {
                    var projectTestConfig = _testInfoConfigRepository.FindById(config.Id);
                    projectTestConfig.ConfigProjectTestId = config.ConfigProjectTestId;
                    projectTestConfig.TestInfoId = config.Id;
                    projectTestConfig.Value = config.Value;
                }
            }
            _unitOfWork.SaveChanges();
            return Ok();
        }
        #endregion
    }
}