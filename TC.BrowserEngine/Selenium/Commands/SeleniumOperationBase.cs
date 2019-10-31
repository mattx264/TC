using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace TC.BrowserEngine.Selenium.Commands
{
    public class SeleiumOperationBase
    {
        internal readonly IWebDriver _driver;

        internal SeleiumOperationBase(IWebDriver driver)
        {
            _driver = driver;
        }
       
    }
}
