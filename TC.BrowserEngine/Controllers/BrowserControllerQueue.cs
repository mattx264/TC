using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC.BrowserEngine.Helpers.Enums;
using TC.BrowserEngine.Services;
using TC.BrowserEngine.Signal;
using TC.Common.DTO;
using TC.Common.Selenium;

namespace TC.BrowserEngine.Controllers
{
    public interface IBrowserControllerQueue
    {
        public int ActiveBrowsers { get; set; }
        public Queue<IBrowserController> BrowserControllers { get; set; }
        public int GetCount();
        public void AddNewBrowser(CommandMessage commandMessage);
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

        public void AddNewBrowser(CommandMessage commandMessage)
        {
          
            var browser = new BrowserController();
            browser.Setup(BrowserType.Chrome, commandMessage);
            BrowserControllers.Enqueue(browser);
            Task.Run(() => StartBrowserFromQueue());
                  
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
