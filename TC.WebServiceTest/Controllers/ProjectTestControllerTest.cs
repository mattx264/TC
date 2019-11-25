﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
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

namespace TC.WebServiceTest.Controllers
{
    class ProjectTestControllerTest
    {
        private TestingCenterDbContext context;
        private ProjectTestController projectTestController;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<TestingCenterDbContext>()
               .UseInMemoryDatabase(databaseName: "Add_writes_to_database")
               .Options;
            context = new TestingCenterDbContext(options);
            TestInfoRepository testInfoRepository = new TestInfoRepository(context);
            projectTestController = new ProjectTestController(testInfoRepository);


        }
        [Test]
        public void UserWithoutValidToken()
        {

            var respose = PostSimpleRequestion();
            // Project not exist return 405
            Assert.AreEqual(respose, new StatusCodeResult(405));
        }
        [Test]
        public void ProjectNotExist()
        {

            var respose = PostSimpleRequestion();
            // Project not exist return 400
            Assert.AreEqual(respose, new NotFoundResult());
        }
        [Test]
        public void UserNotPartOfProject()
        {

            var respose = PostSimpleRequestion();
            // Project not exist return 400
            Assert.AreEqual(respose, new NotFoundResult());
        }
        private IActionResult PostSimpleRequestion()
        {
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