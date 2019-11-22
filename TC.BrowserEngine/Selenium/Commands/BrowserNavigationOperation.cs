using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TC.Common.Selenium;

namespace TC.BrowserEngine.Selenium.Commands
{
    public class BrowserNavigationOperation : SeleiumOperationBase
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
                _driver.Navigate().GoToUrl(url);
                _driver.Manage().Window.Maximize();
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
