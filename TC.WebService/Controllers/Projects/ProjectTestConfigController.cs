using Microsoft.AspNetCore.Mvc;
using TC.DataAccess;
using TC.DataAccess.Repositories;
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
        public ProjectTestConfigController(IProjectTestConfigRepository projectTestConfigRepository, IConfigProjectTestRepository configProjectTestRepository,IUnitOfWork unitOfWork)
        {
            _projectTestConfigRepository = projectTestConfigRepository;
            _configProjectTestRepository = configProjectTestRepository;
            _unitOfWork = unitOfWork;
        }
        #endregion
        #region GET
        [HttpGet]
        public IActionResult Get(int projectId)
        {
            if(projectId == 0)
            {
                return BadRequest("Project Id is required");
            }
            var projectTestConfig = _projectTestConfigRepository.GetByProjectId(projectId);
            if(projectTestConfig == null)
            {
                return BadRequest("Project not found");
            }
            return Ok(new ProjectTestConfigViewModel(projectTestConfig));
        }
        #endregion
        #region POST
        public IActionResult Post(ProjectTestConfigViewModel viewModel)
        {
            var configProjectTest = _configProjectTestRepository.FindById(viewModel.ConfigProjectTestId);
            if (configProjectTest == null)
            {
                return BadRequest("Config project cannot be null");
            }
            //validate if valus is correct with type
            switch (configProjectTest.Type)
            {
                case ConfigProjectTest.ConfigProjectTestEnum.Boolean:
                    bool result;
                    bool.TryParse(viewModel.Value, out result);
                    if (result == false)
                    {
                        return BadRequest("Value boolean is not valid");
                    }
                    break;
                case ConfigProjectTest.ConfigProjectTestEnum.List:
                    // OPEN QUESTION
                    break;
                case ConfigProjectTest.ConfigProjectTestEnum.String:
                    // String cannot be invalid
                    break;

            }
            _projectTestConfigRepository.Create(new ProjectTestConfig()
            {
                ConfigProjectTestId = viewModel.ConfigProjectTestId,
                ProjectId = viewModel.ProjectId,
                Value = viewModel.Value
            });
            _unitOfWork.SaveChanges();
            return Ok();
        }
        #endregion
    }
}