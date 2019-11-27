using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using TC.BrowserEngine.Controllers;
using TC.BrowserEngine.Helpers.Enums;
using TC.Common.Selenium;
using TC.Common.Selenium.WebDriverOperation;

namespace TC.BrowserEngine.Signal
{
   
    public class BrowserControllerPlug : SignalClientBase
    {
        // TODO move to config
        public int maxBrowserOpen= 2;
        private BrowserController browserController;
        private IBrowserControllerFactory _browserControllerFactory;

        public BrowserControllerPlug(string hubName, IBrowserControllerFactory browserControllerFactory) : base(hubName)
        {
            _browserControllerFactory = browserControllerFactory;
            HubConnection connection = StartAsync().GetAwaiter().GetResult();
            try
            {
                if (connection == null)
                {
                    //TODO what to do when connectio is null?
                    return;
                }
                connection.On("ReciveTriggerTest", async (int testId, List<SeleniumCommand> commands) =>
                {
                    ReciveTriggerTest(testId, commands);
                });
                connection.On("ReciveCommand", async (List<SeleniumCommand> commands) =>
                {
                    ReciveCommand(commands);
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                connection.InvokeAsync("SendError", ex);
            }
        }
        public void ReciveTriggerTest(int testId, List<SeleniumCommand> commands)
        {
            Console.WriteLine(testId);
            browserController = new BrowserController(BrowserType.Chrome);
            browserController.Start();
            browserController.RunCommandProcessor(commands);
            browserController = null;
        }
        public void ReciveCommand(List<SeleniumCommand> commands)
        {
            while (_browserControllerFactory.GetCount() > maxBrowserOpen)
            {
                Thread.Sleep(1000);
                
            }
            var browserController = _browserControllerFactory.AddNewBrowser();
            browserController.Start();
            browserController.ExecCommand(commands);
            var lastCommand = commands[commands.Count - 1];
            if (lastCommand.WebDriverOperationType == WebDriverOperationType.BrowserNavigationOperation && lastCommand.OperationId == (int)BrowserOperationEnum.CloseBrowser)
            {
                _browserControllerFactory.RemoveBrowserController(browserController);
                browserController = null;
            }
            ////  if (browserController == null)
            ////  {
            //browserController = new BrowserController(BrowserType.Chrome);

            //browserController.Start().GetAwaiter();
            ////    }
            //// after browser is close run clean up
            //browserController.ExecCommand(commands);
            //var lastCommand = commands[commands.Count - 1];
            //if (lastCommand.WebDriverOperationType == WebDriverOperationType.BrowserNavigationOperation && lastCommand.OperationId == (int)BrowserOperationEnum.CloseBrowser)
            //{
            //    browserController = null;
            //}
        }
    }
}
