using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TC.BrowserEngine.Controllers;

namespace TC.BrowserEngineTest.Controllers
{
    class BrowserControllerFactoryTest
    {
        [Test]
        public void StartNewBrowserTest()
        {
            var browserFactory = new BrowserControllerFactory();
            var browser = browserFactory.AddNewBrowser();
            Assert.IsNotNull(browser);
        }
        [Test]
        public void GetCountTest()
        {
            var browserFactory = new BrowserControllerFactory();
            var browser = browserFactory.AddNewBrowser();
            Assert.AreEqual(browserFactory.GetCount(),1);
         ;
        }
    }
}
