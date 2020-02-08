using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using TC.DataAccess;
using TC.DataAccess.Repositories;
using TC.DataAccess.Repositories.Interfaces;
using TC.Entity.Entities;
using TC.WebService.Controllers.Project;
using TC.WebService.ViewModels.Projects;
using Xunit;

namespace TC.WebServiceTest.Controllers.Projects
{
    public class ProjectTestConfigControllerTest
    {
        private Mock<IProjectTestConfigRepository> _projectTestConfigRepository;
        private Mock<IConfigProjectTestRepository> _configProjectTestRepository;
        private Mock<IUnitOfWork> _unitOfWork;

        #region GET
        [Fact]
        public void Get()
        {
             var projectTestConfigController = GetType();
            _projectTestConfigRepository.Setup(x => x.GetByProjectId(It.IsAny<int>())).Returns(new Mock<IList<ProjectTestConfig>>().Object);
            var response = projectTestConfigController.Get(1);
          
            Assert.IsType<OkObjectResult>(response);
        }       
        [Fact]
        public void Get_Id_Zero()
        {
            var projectTestConfigController = GetType();
            var response = projectTestConfigController.Get(0);

            Assert.IsType<BadRequestObjectResult>(response);
        }

        #endregion
        #region POST
        [Fact]
        public void Post()
        {
            var model = new ProjectTestConfig()
            {
                Id = 0,
                ProjectId = 1,
                Value = "true",
                ConfigProjectTestId = 1
            };
            var projectTestConfigController = GetType();
            _configProjectTestRepository.Setup(x => x.FindById(It.IsAny<int>())).Returns(new Mock<ConfigProjectTest>().Object);

            var reponse = projectTestConfigController.Post(new List<ProjectTestConfigViewModel>() { new ProjectTestConfigViewModel().Convert(model) });
            Assert.IsType<OkResult>(reponse);

        }
        [Fact]
        public void Post_Config_Not_Found()
        {
            var model = new ProjectTestConfig()
            {
                Id = 1,
                ProjectId = 1,
                Value = "wrongbool",
                ConfigProjectTestId = 1
            };
            var projectTestConfigController = GetType();
            var reponse=projectTestConfigController.Post(new List<ProjectTestConfigViewModel>() { new ProjectTestConfigViewModel().Convert(model) });
            Assert.IsType<BadRequestObjectResult>(reponse);
    
        }
        [Fact]
        public void Post_Incorrect_bool_value()
        {
            var model = new ProjectTestConfig()
            {
                Id = 1,
                ProjectId = 1,
                Value = "wrongbool",
                ConfigProjectTestId = 1
            };
            var projectTestConfigController = GetType();
            _configProjectTestRepository.Setup(x => x.FindById(It.IsAny<int>())).Returns(new Mock<ConfigProjectTest>().Object);

            var reponse = projectTestConfigController.Post(new List<ProjectTestConfigViewModel>() { new ProjectTestConfigViewModel().Convert(model) });
            Assert.IsType<BadRequestObjectResult>(reponse);

        }
        private ProjectTestConfigController GetType()
        {
            _projectTestConfigRepository = new Mock<IProjectTestConfigRepository>();
            _projectTestConfigRepository.Setup(x => x.FindById(It.IsAny<int>())).Returns(new Mock<ProjectTestConfig>().Object);
            _configProjectTestRepository = new Mock<IConfigProjectTestRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
            return new ProjectTestConfigController(_projectTestConfigRepository.Object, _configProjectTestRepository.Object, _unitOfWork.Object);
        }
        #endregion
    }
}
