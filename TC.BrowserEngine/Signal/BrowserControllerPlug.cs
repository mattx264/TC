using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;
using TC.BrowserEngine.Controllers;
using TC.BrowserEngine.Helpers.Enums;
using TC.Common.Selenium;
using TC.Common.Selenium.WebDriverOperation;

namespace TC.BrowserEngine.Signal
{
    public class BrowserControllerPlug : SignalClientBase
    {
        private BrowserController browserController;

        public  BrowserControllerPlug(string hubName) : base(hubName)
        {
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
                    Console.WriteLine(testId);
                    browserController = new BrowserController(BrowserType.Chrome);
                    browserController.Start().GetAwaiter();
                    browserController.RunCommandProcessor(commands);
                    browserController = null;
                });
                connection.On("ReciveCommand", async (List<SeleniumCommand> commands) =>
                {
                    if (browserController == null)
                    {
                        browserController = new BrowserController(BrowserType.Chrome);

                        browserController.Start().GetAwaiter();
                    }
                    // after browser is close run clean up
                    browserController.ExecCommand(commands);
                    var lastCommand = commands[commands.Count - 1];
                    if (lastCommand.WebDriverOperationType == WebDriverOperationType.BrowserNavigationOperation && lastCommand.OperationId == (int)BrowserOperationEnum.CloseBrowser)
                    {
                        browserController = null;
                    }

                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                connection.InvokeAsync("SendError", ex);
            }
        }
    }
}
