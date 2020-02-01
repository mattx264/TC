using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TC.BrowserEngine.Controllers;
using TC.BrowserEngine.Helpers.Enums;
using TC.BrowserEngine.Services;
using TC.Common.DTO;
using TC.Common.Selenium;
using TC.Common.Selenium.WebDriverOperation;

namespace TC.BrowserEngineTest.IntegraionTests
{
    public class XhrMonitorTest
    {
        [Test]
        public void SingleCall()
        {
            var commands = new List<SeleniumCommand>();
            commands.Add(new SeleniumCommand()
            {
                WebDriverOperationType = WebDriverOperationType.BrowserNavigationOperation,
                OperationId = 3,
                Values = new string[] { "http://localhost:3000/ajax-test" }
            });
            commands.Add(new SeleniumCommand()
            {
                WebDriverOperationType = WebDriverOperationType.ElementOperationCombo,
                OperationId = 0,
                Values = new string[] { "//*[@id='btnGet']" }
            });
            commands.Add(new SeleniumCommand()
            {
                WebDriverOperationType = WebDriverOperationType.NetworkOperation,
                OperationId = 0,
                Values = new string[] { "http://localhost:3000/ajax-test/get1,GET" }
            });
            commands.Add(new SeleniumCommand()
            {
                WebDriverOperationType = WebDriverOperationType.NetworkOperation,
                OperationId = 1,
                Values = new string[] { "http://localhost:3000/ajax-test/get1,GET,200" }
            });
            commands.Add(new SeleniumCommand()
            {
                WebDriverOperationType = WebDriverOperationType.ElementOperationCombo,
                OperationId = 0,
                Values = new string[] { "//*[@id='btnGet2']" }
            });
            var commandMessage = new CommandMessage()
            {
                ReceiverConnectionId = "receiverTest",
                SenderConnectionId = "senderTest",
                Commands = commands
            };
            var browser = new BrowserController();
            var testprogressEmitter = new Mock<TestProgressEmitter>();
            browser.Setup(BrowserType.Chrome, commandMessage, testprogressEmitter.Object);
            var browserControllerQueue = new BrowserControllerQueue();
            browserControllerQueue.BrowserControllers.Enqueue(browser);
            browserControllerQueue.StartBrowserFromQueue();
        }
    }  
    
}
