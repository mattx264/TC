using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TC.DataAccess;
using TC.DataAccess.Repositories.Interfaces;
using TC.Entity.Entities;
using TC.WebService.ViewModels.Projects;

namespace TC.WebService.Controllers.Project
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectTestConfigController : ControllerBase
    {
        #region private
        private IProjectTestConfigRepository _projectTestConfigRepository;
        private IConfigProjectTestRepository _configProjectTestRepository;
        private IUnitOfWork _unitOfWork;
        #endregion
        #region constructor
        public ProjectTestConfigController(IProjectTestConfigRepository projectTestConfigRepository, IConfigProjectTestRepository configProjectTestRepository, IUnitOfWork unitOfWork)
        {
            _projectTestConfigRepository = projectTestConfigRepository;
            _configProjectTestRepository = configProjectTestRepository;
            _unitOfWork = unitOfWork;
        }
        #endregion
        #region GET
        [HttpGet("{projectId}")]
        public IActionResult Get(int projectId)
        {
            if (projectId == 0)
            {
                return BadRequest("Project Id is required");
            }
            var projectTestConfig = _projectTestConfigRepository.GetByProjectId(projectId);
            if (projectTestConfig.Count == 0)
            {
                var result = _configProjectTestRepository.FindAll().Select(x => new ProjectTestConfigViewModel().Convert(0, projectId, x));
                return Ok(result);
            }
            return Ok(projectTestConfig.Select(x => new ProjectTestConfigViewModel().Convert(x)));
        }
        #endregion
        #region POST
        [HttpPost]
        public IActionResult Post(IList<ProjectTestConfigViewModel> viewModel)
        {
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
                    _projectTestConfigRepository.Create(new ProjectTestConfig()
                    {
                        ConfigProjectTestId = config.ConfigProjectTestId,
                        ProjectId = config.ProjectId,
                        Value = config.Value
                    });
                }
                else
                {
                    var projectTestConfig = _projectTestConfigRepository.FindById(config.Id);
                    projectTestConfig.ConfigProjectTestId = config.ConfigProjectTestId;
                    projectTestConfig.ProjectId = config.ProjectId;
                    projectTestConfig.Value = config.Value;
                }
            }
            _unitOfWork.SaveChanges();
            return Ok();
        }
        #endregion
    }
}