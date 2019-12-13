using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using TC.Common.Selenium;

namespace TC.BrowserEngine.Selenium.Commands
{
    public class Locator : SeleiumOperationBase
    {

        public Locator(IWebDriver driver) :base(driver)
        {
         
        }
        public IWebElement ClassName(string className)
        {
            return _driver.FindElement(By.ClassName(className));
        }
        public IWebElement CssSelector(string css)
        {
            return _driver.FindElement(By.CssSelector(css));
        }
        public IWebElement Id(string id)
        {
            return _driver.FindElement(By.Id(id));
        }
        public IWebElement LinkText(string linkText)
        {
            return _driver.FindElement(By.LinkText(linkText));
        }

        

        public IWebElement Name(string name)
        {
            return _driver.FindElement(By.Name(name));
        }
        public IWebElement PartialLinkText(string partialLinkText)
        {
            return _driver.FindElement(By.PartialLinkText(partialLinkText));
        }
        public IWebElement TagName(string tagName)
        {
            return _driver.FindElement(By.TagName(tagName));
        }
        public IWebElement XPath(string xpath)
        {
            return _driver.FindElement(By.XPath(xpath));
        }
       
        public IWebElement GetByEnum(int operationId, IList<string> values)
        {
            switch ((LocatorsEnum)operationId)
            {
                case LocatorsEnum.ByClassName:
                    return _driver.FindElement(By.ClassName(values[0]));
                case LocatorsEnum.ByCssSelector:
                    return _driver.FindElement(By.CssSelector(values[0]));
                case LocatorsEnum.ById:
                    return _driver.FindElement(By.Id(values[0]));
                case LocatorsEnum.ByLinkText:
                    return _driver.FindElement(By.LinkText(values[0]));
                case LocatorsEnum.ByName:
                    return _driver.FindElement(By.Name(values[0]));
                case LocatorsEnum.ByPartialLinkText:
                    return _driver.FindElement(By.PartialLinkText(values[0]));
                case LocatorsEnum.ByTagName:
                    return _driver.FindElement(By.TagName(values[0]));
                case LocatorsEnum.ByXPath:
                    return _driver.FindElement(By.XPath(values[0]));
                default:
                    throw new Exception("LocatiorsEnum not exist:" + operationId);
            }
        }

        public IReadOnlyCollection<IWebElement> GetElements(LocatorsEnum locator, string value)
        {
            switch (locator)
            {
                case LocatorsEnum.ByClassName:
                    return _driver.FindElements(By.ClassName(value));
                case LocatorsEnum.ByCssSelector:
                    return _driver.FindElements(By.CssSelector(value));
                case LocatorsEnum.ById:
                    return _driver.FindElements(By.Id(value));
                case LocatorsEnum.ByLinkText:
                    return _driver.FindElements(By.LinkText(value));
                case LocatorsEnum.ByName:
                    return _driver.FindElements(By.Name(value));
                case LocatorsEnum.ByPartialLinkText:
                    return _driver.FindElements(By.PartialLinkText(value));
                case LocatorsEnum.ByTagName:
                    return _driver.FindElements(By.TagName(value));
                case LocatorsEnum.ByXPath:
                    return _driver.FindElements(By.XPath(value));
                default:
                    throw new Exception("LocatiorsEnum not exist:"+ locator);
            }
        }

    }

}
