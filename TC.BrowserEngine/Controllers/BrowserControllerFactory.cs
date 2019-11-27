using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TC.BrowserEngine.Helpers.Enums;

namespace TC.BrowserEngine.Controllers
{
    public interface IBrowserControllerFactory
    {
        public int GetCount();
        public IBrowserController AddNewBrowser();
        public void AddNewBrowserToList(IBrowserController browser);
        void RemoveBrowserController(IBrowserController browserController);
    }
    public class BrowserControllerFactory : IBrowserControllerFactory
    {
        private List<IBrowserController> browserControllers;
        public BrowserControllerFactory()
        {
            browserControllers = new List<IBrowserController>();
        }
        public int GetCount()
        {
            return browserControllers.Count();
        }
       
        public IBrowserController AddNewBrowser()
        {

            var browser = new BrowserController(BrowserType.Chrome);
            AddNewBrowserToList(browser);
            return browser;
        }
        public void AddNewBrowserToList(IBrowserController browser)
        {
            browserControllers.Add(browser);
        }

        public void RemoveBrowserController(IBrowserController browserController)
        {
            browserControllers.Remove(browserController);
        }
    }
}
