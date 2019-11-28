using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC.BrowserEngine.Helpers.Enums;
using TC.Common.Selenium;

namespace TC.BrowserEngine.Controllers
{
    public interface IBrowserControllerQueue
    {
        public int ActiveBrowsers { get; set; }
        public Queue<IBrowserController> BrowserControllers { get; set; }
        public int GetCount();
        public void AddNewBrowser(List<SeleniumCommand> commands);
        public void StartBrowserFromQueue();
    }
    public class BrowserControllerQueue : IBrowserControllerQueue
    {
        public Queue<IBrowserController> BrowserControllers { get; set; }      
        public int ActiveBrowsers { get; set; } = 0;
        private int maxActiveBrowsers = 3;
        public BrowserControllerQueue()
        {
            BrowserControllers = new Queue<IBrowserController>();
        }
        public int GetCount()
        {
            return BrowserControllers.Count();
        }

        public void AddNewBrowser(List<SeleniumCommand> commands)
        {
            var browser = new BrowserController();
            browser.Setup(BrowserType.Chrome, commands);
            BrowserControllers.Enqueue(browser);
            Task.Run(() => StartBrowserFromQueue());
            //var lastCommand = commands[commands.Count - 1];
            //if (lastCommand.WebDriverOperationType == WebDriverOperationType.BrowserNavigationOperation && lastCommand.OperationId == (int)BrowserOperationEnum.CloseBrowser)
            //{
            //    _browserControllerFactory.RemoveBrowserController(browserController);
            //    browserController = null;
            //}           
        }
        public virtual void StartBrowserFromQueue()
        {
            if (ActiveBrowsers >= maxActiveBrowsers || BrowserControllers.Count == 0)
            {
                return;
            }
            ActiveBrowsers++;
            var browser = BrowserControllers.Dequeue();
            browser.Start();
            browser.RunCommandProcessor();
            ActiveBrowsers--;
            StartBrowserFromQueue();
        }

    }
}
