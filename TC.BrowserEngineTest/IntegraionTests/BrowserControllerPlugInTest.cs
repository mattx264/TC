using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TC.BrowserEngine.Controllers;
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
                Values = new string[] { "https://www.google.com/" }
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
    }
}
