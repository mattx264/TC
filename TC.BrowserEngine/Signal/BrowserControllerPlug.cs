using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using TC.BrowserEngine.Controllers;
using TC.BrowserEngine.Helpers.Enums;
using TC.BrowserEngine.Services;
using TC.Common.Selenium;
using TC.Common.Selenium.WebDriverOperation;

namespace TC.BrowserEngine.Signal
{
    public delegate void SendTestProgressDelegate();

    public class BrowserControllerPlug : SignalClientBase
    {
        private BrowserController browserController;
        private IBrowserControllerQueue _browserControllerFactory;
        private HubConnection _connection;
        // TODO move to config
        public int maxBrowserOpen= 2;       
        public TestProgressSubscriber _subsciber;
        public SendTestProgressDelegate _sendTestProgressDelegate;

        public BrowserControllerPlug(string hubName, IBrowserControllerQueue browserControllerFactory) : base(hubName)
        {
            _browserControllerFactory = browserControllerFactory;
            _connection = StartAsync().GetAwaiter().GetResult();
            _sendTestProgressDelegate = SendTestProgress;
            _subsciber = new TestProgressSubscriber(_sendTestProgressDelegate);
            TestProgressSubscriber.Set(new Guid(), _subsciber);
            
            
            try
            {
                if (_connection == null)
                {
                    //TODO what to do when connectio is null?
                    return;
                }
                _connection.On("ReciveTriggerTest", async (int testId, List<SeleniumCommand> commands) =>
                {
                    ReciveTriggerTest(testId, commands);
                });
                _connection.On("ReciveCommand", async (List<SeleniumCommand> commands) =>
                {
                    ReciveCommand(commands);
                });
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _connection.InvokeAsync("SendError", ex);
            }
        }
        public void SendTestProgress()
        {
            _connection.SendAsync("TestProgress");
        }
        public void ReciveTriggerTest(int testId, List<SeleniumCommand> commands)
        {

            _browserControllerFactory.AddNewBrowser(commands,this);
            
        }
        public void ReciveCommand(List<SeleniumCommand> commands)
        {
           
             _browserControllerFactory.AddNewBrowser(commands,this);
          
        }
    }
}
