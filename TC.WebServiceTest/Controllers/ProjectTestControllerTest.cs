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

namespace TC.WebServiceTest.Controllers
{
    public class ProjectTestControllerTest
    {
        private TestingCenterDbContext context;
        private ProjectTestController projectTestController;


        public void Setup()
        {
            var options = new DbContextOptionsBuilder<TestingCenterDbContext>()
               .UseInMemoryDatabase(databaseName: "Add_writes_to_database")
               .Options;
            context = new TestingCenterDbContext(options);
            TestInfoRepository testInfoRepository = new TestInfoRepository(context);
            var userHelper = new Mock<IUserHelper>();

            projectTestController = new ProjectTestController(testInfoRepository, userHelper.Object);


        }
        [Fact]
        public void ProjectNotExist()
        {
           
            var respose = PostSimpleRequestion();
            // Project not exist return 400
          //  Assert.Equal(respose, new NotFoundResult());
        }
        [Fact]
        public void UserNotPartOfProject()
        {
          
            var respose = PostSimpleRequestion();
            // Project not exist return 400
           // Assert.Equal(respose, new NotFoundResult());
        }
        private IActionResult PostSimpleRequestion()
        {
            Setup();
            return projectTestController.Post(new ProjectTestViewModel()
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
