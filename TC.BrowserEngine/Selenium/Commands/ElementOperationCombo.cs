using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using TC.Common.Selenium;
using TC.Common.Selenium.WebDriverOperation;

namespace TC.BrowserEngine.Selenium.Commands
{
    public class ElementOperationCombo : SeleiumOperationBase
    {
        private Locator _locator;

        internal ElementOperationCombo(IWebDriver driver) : base(driver)
        {
            _locator = new Locator(driver);
        }
        public void SendKeys(string selector,string value)
        {
            var element = _locator.XPath(selector);
            if (value.Contains("Keys."))
            {
                //TODO finish this
                element.SendKeys(Keys.Enter);
            }
            else
            {
                
                element.SendKeys(value);
            }
        }
        public void Click(string selector)
        {
            var element = _locator.XPath(selector);
            element.Click();
        }

        public void GetByEnum(int operationId, IList<string> values)
        {
            switch ((ElementOperationComboEnum)operationId)
            {
                case ElementOperationComboEnum.SendKeys:
                    this.SendKeys(values[0], values[1]);
                    break;
                case ElementOperationComboEnum.Click:
                    this.Click(values[0]);
                    break;
            }
        }
    }
}
