using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
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
        #region  Drop down list select
        public void SelectByText(IWebElement element, string text)
        {
            var selectElement = new SelectElement(element);
            selectElement.SelectByText(text);
        }
        public void SelectByValue(IWebElement element, string value)
        {
            var selectElement = new SelectElement(element);
            selectElement.SelectByValue(value);         
        }
        #endregion // Drop down list select
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
                case ElementOperationEnum.SelectByText:
                    this.SelectByText(element, values[0]);
                    break;
                case ElementOperationEnum.SelectByValue:
                    this.SelectByValue(element, values[0]);
                    break;
            }
        }

    }
}
