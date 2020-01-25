using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TC.Common.Selenium;
using TC.Common.Selenium.WebDriverOperation;

namespace TC.BrowserEngine.Selenium.Commands
{
    public class BrowserNavigationOperation : SeleiumOperationBase, IOperationsMethod
    {
        public BrowserNavigationOperation(IWebDriver driver) : base(driver)
        {
        }
        public void GoToUrl(string url)
        {
            if (!url.Contains("http"))
            {
                throw new Exception("Url is incorect");
            }
            else
            {
                var xhrMonitor = JavaScript.JavaScript.XhrMonitor();
                _driver.Navigate().GoToUrl(url);            
                ((IJavaScriptExecutor)_driver).ExecuteScript(xhrMonitor);
            }
        }
        public void WaitUntilBrowserReady()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            wait.Until((x) =>
            {
                return ((IJavaScriptExecutor)_driver).ExecuteScript("return document.readyState").Equals("complete");
            });
        }
        public void CloseBrowser()
        {
            _driver.Close();
        }
        public void GetByEnum(int operationId, IList<string> values)
        {
            switch ((BrowserOperationEnum)operationId)
            {
                case BrowserOperationEnum.GoToUrl:
                    this.GoToUrl(values[0]);
                    break;
                case BrowserOperationEnum.WaitUntilBrowserReady:
                    this.WaitUntilBrowserReady();
                    break;
                case BrowserOperationEnum.CloseBrowser:
                    this.CloseBrowser();
                    break;
            }
        }

    }
}
