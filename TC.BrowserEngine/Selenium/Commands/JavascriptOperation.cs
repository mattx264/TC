using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using System.Collections.Generic;

namespace TC.BrowserEngine.Selenium.Commands
{
    public class JavascriptOperation : SeleiumOperationBase
    {
        public JavascriptOperation(IWebDriver driver) : base(driver)
        {
        }
        public void RunJS(IList<string> values)
        {
            _driver.ExecuteJavaScript(values[0]);
        }

    }
}
