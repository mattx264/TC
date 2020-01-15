using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using TC.Common.Selenium;

namespace TC.BrowserEngine.Selenium.Commands
{
    public class BrowserOperation : SeleiumOperationBase, IOperationsMethod
    {
        public BrowserOperation(IWebDriver driver) : base(driver)
        {
        }
        public Screenshot GetScreenshot()
        {
            return ((ITakesScreenshot)_driver).GetScreenshot();
            //Save the screenshot
            // image.SaveAsFile("C:/temp/Screenshot.png", ImageFormat.Png);


        }
        public void GetByEnum(int operationId, IList<string> values)
        {

            //  switch ((BrowserOperationEnum)operationId)
            //  {
            // case BrowserOperationEnum.GetScreenshot:
            //    break;

            //  }
        }
    }
}