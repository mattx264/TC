using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TC.BrowserEngine.Controllers;
using TC.BrowserEngine.Helpers.Enums;
using TC.BrowserEngine.Selenium;
using TC.BrowserEngine.Services;
using TC.BrowserEngine.Signal;
using TC.Common.DTO;
using TC.Common.Selenium;
using TC.Common.Selenium.WebDriverOperation;

namespace TC.BrowserEngineTest.IntegraionTests
{
    public class ScreenshotTest
    {
        [Test]
        public void TakeScreenshot()
        {
            
             //  var browserControllerPlugIn = new BrowserControllerPlug("test", new BrowserControllerQueue());
           
            var commands = new List<SeleniumCommand>();
            commands.Add(new SeleniumCommand()
            {
                WebDriverOperationType = WebDriverOperationType.BrowserNavigationOperation,
                OperationId = 3,
                Values = new string[] { "http://localhost:3000" }
            });
            commands.Add(new SeleniumCommand()
            {
                WebDriverOperationType = WebDriverOperationType.BrowserOperation,
                OperationId = (int)BrowserOperationEnum.GetScreenshot
            });
            var commandMessage = new CommandMessage()
            {
                ReceiverConnectionId = "receiverTest",
                SenderConnectionId = "senderTest",
                Commands = commands
            };
            var browser = new BrowserController();
            var testprogressEmitter=new Mock<ITestProgressEmitter>();
            browser.Setup(BrowserType.Chrome, commandMessage, testprogressEmitter.Object);
            var browserControllerQueue = new BrowserControllerQueue();
            browserControllerQueue.BrowserControllers.Enqueue(browser);
            browserControllerQueue.StartBrowserFromQueue();


           // browserControllerPlugIn.ReciveCommand(commandMessage);
        }
    }
}
