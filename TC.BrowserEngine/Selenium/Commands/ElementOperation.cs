using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using TC.Common.Selenium;

namespace TC.BrowserEngine.Selenium.Commands
{
    public class ElementOperation : SeleiumOperationBase
    {
        internal ElementOperation(IWebDriver driver) : base(driver)
        {
        }
        public void SendKeys(IWebElement element,string keys)
        {
            element.SendKeys(keys);
        }
        public void Click(IWebElement element)
        {
            element.Click();
        }

        public void GetByEnum(int operationId, IList<string> values,IWebElement element)
        {
            switch ((ElementOperationEnum)operationId)
            {
                case ElementOperationEnum.SendKeys:
                    this.SendKeys(element,values[0]);
                    break;
                case ElementOperationEnum.Click:
                    this.Click(element);
                    break;

            }
        }
    }
}
