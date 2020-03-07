using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;
using TC.BrowserEngine.Controllers;
using TC.BrowserEngine.Services;
using TC.Common.DTO;

namespace TC.BrowserEngine.Signal
{
    public delegate void SendTestProgressDelegate(ITestProgress testProgress);
    public delegate void SendTestProgressImageDelegate(ScreenshotTestProgress testProgress);

    public class BrowserControllerPlug : SignalClientBase
    {
        private IBrowserControllerQueue _browserControllerFactory;
        private HubConnection _connection;
        // TODO move to config
        public int maxBrowserOpen = 2;
        public TestProgressSubscriber _subsciber;
        public SendTestProgressDelegate _sendTestProgressDelegate;
        public SendTestProgressImageDelegate _sendTestProgressImageDelegate;
        public BrowserControllerPlug(string hubName, IBrowserControllerQueue browserControllerFactory) : base(hubName)
        {
            _browserControllerFactory = browserControllerFactory;
            _connection = StartAsync().GetAwaiter().GetResult();
            _sendTestProgressDelegate = SendTestProgress;
            _sendTestProgressImageDelegate = SendTestProgressScreenshotAsync;
            _subsciber = new TestProgressSubscriber(_sendTestProgressDelegate, _sendTestProgressImageDelegate);
            TestProgressSubscriber.Set(new Guid(), _subsciber);

            try
            {
                if (_connection == null)
                {
                    //TODO what to do when connectio is null?
                    return;
                }
                _connection.On("ReciveTriggerTest", (int testId, CommandMessage commandMessage) =>
                {
                    ReciveTriggerTest(testId, commandMessage);
                });
                _connection.On("ReciveCommand", (CommandMessage commandMessage) =>
                {
                    ReciveCommand(commandMessage);
                });

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _connection.InvokeAsync("SendError", ex);
            }
        }
        public void SendTestProgress(ITestProgress testProgress)
        {
            _connection.SendAsync("TestProgress", new TestProgressMessage()
            {
                IsSuccesful = testProgress.IsSuccesfull,
                CommandTestGuid = testProgress.command.Guid,
                SenderConnectionId = testProgress.senderConnectionId,
                TestRunHistoryId = testProgress.TestRunHistoryId,
                Message = testProgress.Message
            });
        }
        public void SendTestProgressScreenshotAsync(ScreenshotTestProgress testProgress)
        {
            FileUploadService.UploadScreenshotAsync(testProgress).GetAwaiter();
        }
        public void ReciveTriggerTest(int testId, CommandMessage commandMessage)
        {

            _browserControllerFactory.AddNewBrowser(commandMessage);

        }
        public void ReciveCommand(CommandMessage commandMessage)
        {

            _browserControllerFactory.AddNewBrowser(commandMessage);

        }
    }
}
