using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TC.DataAccess;
using TC.DataAccess.Repositories.Interfaces;
using TC.Entity.Entities;
using TC.WebService.Services.Interface;
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
        private IProjectTestConfigService _projectTestConfigService;
        private IUnitOfWork _unitOfWork;
     

        public TestInfoConfigController(
            ITestInfoConfigRepository testInfoConfigRepository,
            IConfigProjectTestRepository configProjectTestRepository,
            IProjectTestConfigService projectTestConfigService,
            IUnitOfWork unitOfWork
            )
        {
            _testInfoConfigRepository = testInfoConfigRepository;
            _configProjectTestRepository = configProjectTestRepository;
            _projectTestConfigService = projectTestConfigService;
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region GET
        [HttpGet("{testInfoId}")]
        public ActionResult<IList<TestInfoConfigViewModel>> Get(int testInfoId)
        {
            if (testInfoId == 0)
            {
                throw new ArgumentNullException(nameof(testInfoId));
            }

            return Ok(_projectTestConfigService.GetTestConfigByTestId(testInfoId));


        }
        #endregion

        #region POST
        [HttpPost]
        public IActionResult Post(IList<TestInfoConfigViewModel> viewModel)
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
                        TestInfoId = config.TestInfoId,
                        Value = config.Value
                    });
                }
                else
                {
                    var projectTestConfig = _testInfoConfigRepository.FindById(config.Id);
                    if (projectTestConfig == null)
                    {
                        _testInfoConfigRepository.Create(new TestInfoConfig()
                        {
                            ConfigProjectTestId = config.ConfigProjectTestId,
                            TestInfoId = config.TestInfoId,
                            Value = config.Value
                        });
                    }
                    else
                    {
                        projectTestConfig.Value = config.Value;
                    }
                }
            }
            _unitOfWork.SaveChanges();
            return Ok();
        }
        #endregion
    }
}