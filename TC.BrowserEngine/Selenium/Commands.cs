﻿using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using TC.BrowserEngine.Selenium.Commands;
using TC.Common.Selenium;
using TC.Common.Selenium.WebDriverOperation;

namespace TC.BrowserEngine.Selenium
{
    public class CommandProcessor
    {
        private IWebDriver _driver;
        IWebElement element;
        public CommandProcessor(IWebDriver driver)
        {
            _driver = driver;
        }
        /// <summary>
        /// Run full test from start to end and close browser.
        /// </summary>
        /// <param name="SeleniumCommands"></param>
        public void Start(List<SeleniumCommand> SeleniumCommands)
        {
            
            foreach (var command in SeleniumCommands)
            {

                element = RunCommand(command);
            }
            _driver.Close();
        }
        /// <summary>
        /// Exec list of commands but browser will wait for next call.
        /// </summary>
        /// <param name="SeleniumCommands"></param>
        public void Exec(List<SeleniumCommand> SeleniumCommands)
        {
           
            foreach (var command in SeleniumCommands)
            {

                element = RunCommand(command);
            }
           
        }
        private IWebElement RunCommand(SeleniumCommand command)
        {
            if (_driver == null)
            {
                throw new Exception("Driver can not be null");
            }
            switch (command.WebDriverOperationType)
            {
                case WebDriverOperationType.Locators:
                    return new Locator(_driver).GetByEnum(command.OperationId, command.Values);

                case WebDriverOperationType.BrowserNavigationOperation:
                    new BrowserNavigationOperation(_driver).GetByEnum(command.OperationId, command.Values);
                    return null;
                case WebDriverOperationType.ElementOperation:
                    new ElementOperation(_driver).GetByEnum(command.OperationId, command.Values, element);
                    return null;
                case WebDriverOperationType.ElementOperationCombo:
                    new ElementOperationCombo(_driver).GetByEnum(command.OperationId, command.Values);
                    return null;
                    
            }
            return null;
        }

        public string GetPageSource()
        {
           return _driver.PageSource;
        }
    }
}
