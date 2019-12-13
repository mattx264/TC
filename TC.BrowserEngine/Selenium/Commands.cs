using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using TC.BrowserEngine.Controllers;
using TC.BrowserEngine.Selenium.Commands;
using TC.BrowserEngine.Services;
using TC.Common.DTO;
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
        public void Start(CommandMessage commandMessage)
        {
            TestProgressEmitter testProgressEmitter = new TestProgressEmitter();

            foreach (var command in commandMessage.Commands)
            {
                TestProgress testProgress = new TestProgress()
                {
                    senderConnectionId = commandMessage.SenderConnectionId,
                    command = command
                };
                try
                {
                    element = RunCommand(command);
                    testProgress.IsSuccesfull = true;
                    testProgressEmitter.CommandComplete(testProgress);
                }
                catch(Exception ex)
                {
                    // TODO - should it break when there is error ?????
                    testProgress.IsSuccesfull = false;
                    testProgress.Message = ex.Message;
                    testProgressEmitter.CommandComplete(testProgress);
                }
               
            }
           // _driver.Close();
        }
       
        private IWebElement RunCommand(SeleniumCommand command)
        {
            if (_driver == null)
            {
                throw new Exception("Driver can not be null");
            }
            // TODO config
            // _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

           

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
                case WebDriverOperationType.JavascriptOperation:
                    new JavascriptOperation(_driver).RunJS(command.Values);
                    return null;
            }
            
            return null;
        }

        public string GetPageSource()
        {
           return _driver.PageSource;
        }
       
        public static void WaitForAjax(IWebDriver driver, String action)
        {
           // driver.Manage().Timeouts().ImplicitWaitsetScriptTimeout(5, TimeUnit.SECONDS);
            ((IJavaScriptExecutor)driver).ExecuteAsyncScript(
                    "var callback = arguments[arguments.length - 1];" +
                            "var xhr = new XMLHttpRequest();" +
                            "xhr.open('POST', '/" + action + "', true);" +
                            "xhr.onreadystatechange = function() {" +
                            "  if (xhr.readyState == 4) {" +
                            "    callback(xhr.responseText);" +
                            "  }" +
                            "};" +
                            "xhr.send();");
        }
    }
}
