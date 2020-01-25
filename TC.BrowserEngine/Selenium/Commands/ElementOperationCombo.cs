using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using TC.Common.Selenium.WebDriverOperation;

namespace TC.BrowserEngine.Selenium.Commands
{
    public class ElementOperationCombo : SeleiumOperationBase, IOperationsMethod
    {
        private Locator _locator;

        internal ElementOperationCombo(IWebDriver driver) : base(driver)
        {
            _locator = new Locator(driver);
        }
        public void SendKeys(string selector, string value)
        {
            var element = _locator.XPath(selector);
            if (value.Contains("Keys."))
            {
                //TODO finish this - add more keys
                switch (value)
                {
                    case "Keys.ENTER":
                        element.SendKeys(Keys.Enter);
                        break;
                    case "Keys.TAB":
                        element.SendKeys(Keys.Tab);
                        break;
                }
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
        public void DropDownSelectByValue(string selector, string value)
        {
            var element = _locator.XPath(selector);

            var selectElement = new SelectElement(element);
            selectElement.SelectByValue(value);

        }

        public  void GetByEnum(int operationId, IList<string> values)
        {
            
            switch ((ElementOperationComboEnum)operationId)
            {
                case ElementOperationComboEnum.Click:
                    this.Click(values[0]);
                    break;
                case ElementOperationComboEnum.SendKeys:
                    this.SendKeys(values[0], values[1]);
                    break;              
                case ElementOperationComboEnum.DropDownSelectByValue:
                    this.DropDownSelectByValue(values[0], values[1]);
                    break;
            }
        }
    }
}
