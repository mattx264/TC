using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TC.BrowserEngine.Controllers;
using TC.Common.DTO;

namespace TC.BrowserEngineTest.Controllers
{
    [TestFixture]
    class BrowserControllerQueueTest
    {
        [Test]
        public void StartBrowserFromQueueTest()
        {
            var browserFactory = new Mock<IBrowserControllerQueue>();
            var testQueue = new Queue<IBrowserController>();
            var browserController = new Mock<IBrowserController>();
            browserController.Setup(x => x.Start());
            testQueue.Enqueue(browserController.Object);
            browserFactory.SetupGet(x => x.BrowserControllers).Returns(testQueue);
            browserFactory.Object.StartBrowserFromQueue();
            Assert.AreEqual(browserFactory.Object.GetCount(), 0);
            Assert.AreEqual(browserFactory.Object.ActiveBrowsers, 0);
        }
        [Test]
        public void GetCountTest()
        {
            var browserFactory = new Mock<BrowserControllerQueue>();
            browserFactory.CallBase = true;
            browserFactory.Setup(x => x.StartBrowserFromQueue());//.Callback(() => { });
            browserFactory.Object.AddNewBrowser(new CommandMessage());
            Assert.AreEqual(browserFactory.Object.GetCount(), 1);
            browserFactory.Object.AddNewBrowser(new CommandMessage());
            Assert.AreEqual(browserFactory.Object.GetCount(), 2);

        }
    }
}
