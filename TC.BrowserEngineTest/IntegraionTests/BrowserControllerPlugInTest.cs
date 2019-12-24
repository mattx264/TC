using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TC.BrowserEngine.Controllers;
using TC.BrowserEngine.Helpers.Enums;
using TC.BrowserEngine.Selenium;
using TC.BrowserEngine.Signal;
using TC.Common.DTO;
using TC.Common.Selenium;
using TC.Common.Selenium.WebDriverOperation;

namespace TC.BrowserEngineTest.IntegraionTests
{
    class BrowserControllerPlugInTest
    {
        [Test]
        public void UpdateTest()
        {
            var browserControllerPlugIn = new BrowserControllerPlug("test", new BrowserControllerQueue());
            var commands = new List<SeleniumCommand>();
            commands.Add(new SeleniumCommand()
            {
                WebDriverOperationType = WebDriverOperationType.BrowserNavigationOperation,
                OperationId = 3,
                Values = new string[] { "https://www.onet.pl/" }
            });

            var commandMessage = new CommandMessage()
            {
                ReceiverConnectionId = "receiverTest",
                SenderConnectionId = "senderTest",
                Commands = commands
            };

            browserControllerPlugIn.ReciveCommand(commandMessage);
            // TODO add Assert
           
        }
        [Test]
        public void CheckIfBrowserIsCloseAfterSimpleCommand()
        {
            var commands = new List<SeleniumCommand>();
            commands.Add(new SeleniumCommand()
            {
                WebDriverOperationType = WebDriverOperationType.BrowserNavigationOperation,
                OperationId = 3,
                Values = new string[] { "https://www.onet.pl/" }
            });
            var commandMessage = new CommandMessage()
            {
                ReceiverConnectionId = "receiverTest",
                SenderConnectionId = "senderTest",
                Commands = commands
            };
            var browser = new BrowserController();
            browser.Setup(BrowserType.Chrome, commandMessage);
            var browserControllerQueue = new BrowserControllerQueue();
            browserControllerQueue.BrowserControllers.Enqueue(browser);
            browserControllerQueue.StartBrowserFromQueue();
           
            Assert.IsFalse(browser.IsBrowserRunning());         
        }
        [Test]
        public void BrowserDriverCreateTest()
        {
            var commands = new List<SeleniumCommand>();
            commands.Add(new SeleniumCommand()
            {
                WebDriverOperationType = WebDriverOperationType.BrowserNavigationOperation,
                OperationId = 3,
                Values = new string[] { "https://int-enrollment.aiuniv.edu/#/LandingPage" }
            });
            commands.Add(new SeleniumCommand()
            {
                WebDriverOperationType = WebDriverOperationType.JavascriptOperation,
                Values = new string[] { "alert(getCampusInformation().CampusId);" }

            });
            commands.Add(new SeleniumCommand()
            {
                WebDriverOperationType = WebDriverOperationType.JavascriptOperation,
                Values = new string[] { "myF();" }

            });
            commands.Add(new SeleniumCommand()
            {
                WebDriverOperationType = WebDriverOperationType.JavascriptOperation,
                Values = new string[] { "alert(myF())" }

            });
            var commandMessage = new CommandMessage()
            {
                ReceiverConnectionId = "receiverTest",
                SenderConnectionId = "senderTest",
                Commands = commands
            };
            var browser = new BrowserController();
            browser.Setup(BrowserType.Chrome, commandMessage);
            var browserControllerQueue = new BrowserControllerQueue();
            browserControllerQueue.BrowserControllers.Enqueue(browser);
            browserControllerQueue.StartBrowserFromQueue();

            Assert.IsFalse(browser.IsBrowserRunning());
        }
    }
}
