using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Text;
using TC.DataAccess.DatabaseContext;
using System.Linq;
using TC.WebService.Controllers;
using Moq;
using TC.DataAccess.Repositories;
using TC.WebService.ViewModels;
using TC.Common.Selenium;
using System.Threading.Tasks;
using TC.Common.Selenium.WebDriverOperation;
using Microsoft.AspNetCore.Mvc;
using TC.Entity.Entities;
using TC.WebService.Helpers;
using Xunit;
using TC.DataAccess;
using TC.Entity.Entities.Projects;

namespace TC.WebServiceTest.Controllers
{
    public class ProjectTestControllerTest
    {
        private TestingCenterDbContext context;


        public ProjectTestController Setup()
        {
            var options = new DbContextOptionsBuilder<TestingCenterDbContext>()
               .UseInMemoryDatabase(databaseName: "Add_writes_to_database")
               .Options;
            context = new TestingCenterDbContext(options);
            TestInfoRepository testInfoRepository = new TestInfoRepository(context);
            var userHelper = new Mock<IUserHelper>();
            var unitOfWork = new Mock<IUnitOfWork>();
            var projectRepository = new Mock<IProjectRepository>();
            projectRepository.Setup(x => x.GetProjectByUser(It.IsAny<string>(), It.IsAny<int>())).Returns(() => new Project() { 
            TestInfos=new List<TestInfo>()
            });
            return new ProjectTestController(testInfoRepository, projectRepository.Object, unitOfWork.Object, userHelper.Object);
        }
        [Fact]
        public void ProjectOrNotBelongToUserNotExist()
        {
            var options = new DbContextOptionsBuilder<TestingCenterDbContext>()
                .UseInMemoryDatabase(databaseName: "Add_writes_to_database")
                .Options;
            context = new TestingCenterDbContext(options);
            TestInfoRepository testInfoRepository = new TestInfoRepository(context);
            var userHelper = new Mock<IUserHelper>();
            var unitOfWork = new Mock<IUnitOfWork>();
            var projectRepository = new Mock<IProjectRepository>();
            projectRepository.Setup(x => x.GetProjectByUser(It.IsAny<string>(), It.IsAny<int>())).Returns(()=> null);
            var  projectTestController = new ProjectTestController(testInfoRepository, projectRepository.Object, unitOfWork.Object, userHelper.Object);

            var respose = PostSimpleRequestion(projectTestController);
           
            // Project not exist return 400
            Assert.IsType<BadRequestResult>(respose);
        }
        [Fact]
        public void ProjectAdd()
        {
            var projectTestController=Setup();
            var respose = PostSimpleRequestion(projectTestController);
           
            Assert.IsType<CreatedAtActionResult>(respose);
        }
        private IActionResult PostSimpleRequestion(ProjectTestController controller)
        {
           
            return controller.Post(new ProjectTestViewModel()
            {
                ProjectId = 1,
                SeleniumCommands = new List<SeleniumCommand>()
                    {
                        new SeleniumCommand()
                        {
                            OperationId=1,
                            Values=new string[]{"test" },
                            WebDriverOperationType=WebDriverOperationType.BrowserNavigationOperation
                        }
                    }
            });
        }
    }
}
