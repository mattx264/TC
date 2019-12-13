using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace TC.BrowserEngine.Selenium.Commands
{
    public abstract class SeleiumOperationBase
    {
        internal readonly IWebDriver _driver;

        internal SeleiumOperationBase(IWebDriver driver)
        {
            _driver = driver;
        }
       
    }
    public interface IOperationsMethod
    {
        public void GetByEnum(int operationId, IList<string> values);
    }
}
