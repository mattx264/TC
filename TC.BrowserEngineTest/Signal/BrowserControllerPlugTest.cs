using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TC.BrowserEngine.Controllers;
using TC.BrowserEngine.Helpers.Enums;
using TC.BrowserEngine.Signal;
using TC.Common.DTO;
using TC.Common.Selenium;

namespace TC.BrowserEngineTest.Signal
{
    public class BrowserControllerPlugTest
    {

        [Test]
        public void ReciveCommandTest()
        {
            //    var browserController = new Mock<IBrowserController>();
            //    browserController.Setup(x => x.Start());
            //    var browserControllerFactoryMock = new Mock<IBrowserControllerFactory>();
            //    browserControllerFactoryMock.Setup(x => x.AddNewBrowser())
            //        .Returns(() =>
            //        {
            //            browserControllerFactoryMock.Object.AddNewBrowserToList(new BrowserController(BrowserType.Chrome));
            //            return browserController.Object;
            //        });

            //    var browserControllerPlug = new BrowserControllerPlug("hub name", browserControllerFactoryMock.Object);
            //    browserControllerPlug.maxBrowserOpen = 2;
            //    var commands = new List<SeleniumCommand>();
            //    browserControllerPlug.ReciveCommand(commands);
            //    //Assert.AreEqual(browserControllerFactoryMock.Object.GetCount(), 1);
            //    browserControllerPlug.ReciveCommand(commands);
            //   // Assert.AreEqual(browserControllerFactoryMock.Object.GetCount(), 2);
        }
        [Test, Description("Test reciveCommand function when there is more than max brwoser open")]
        public void ReciveCommandWaitTest()
        {
            Task.Delay(2000).ContinueWith((task) =>
            {
            });

            var browserControllerFactoryMock = new Mock<IBrowserControllerQueue>();
            browserControllerFactoryMock.Setup(x => x.GetCount())
                .Returns(() =>
                {
                    return 1;
                });
            var browserControllerPlug = new BrowserControllerPlug("hub name", browserControllerFactoryMock.Object);
            browserControllerPlug.maxBrowserOpen = 1;
            var commandMessage = new CommandMessage();
            browserControllerPlug.ReciveCommand(commandMessage);



        }
    }
}
