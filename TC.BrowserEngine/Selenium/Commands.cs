using OpenQA.Selenium;
using System;
using System.Linq;
using TC.BrowserEngine.Helpers;
using TC.BrowserEngine.Selenium.Commands;
using TC.BrowserEngine.Services;
using TC.Common.DTO;
using TC.Common.Selenium;
using TC.Common.Selenium.WebDriverOperation;

namespace TC.BrowserEngine.Selenium
{
    public class CommandProcessor
    {
        private TestProgressEmitter _testProgressEmitter;
        private IWebDriver _driver;
        IWebElement element;

        public CommandProcessor(IWebDriver driver, TestProgressEmitter testProgressEmitter)
        {
            _testProgressEmitter = testProgressEmitter;
            _driver = driver;
        }

        /// <summary>
        /// Run full test from start to end and close browser.
        /// </summary>
        /// <param name="SeleniumCommands"></param>
        public void Start(CommandMessage commandMessage)
        {


            foreach (var command in commandMessage.Commands)
            {
                ITestProgress testProgress = new TestProgress()
                {
                    senderConnectionId = commandMessage.SenderConnectionId,
                    command = command,
                    TestRunHistoryId = commandMessage.TestRunHistoryId.Value
                };
                try
                {
                    if (command.WebDriverOperationType == WebDriverOperationType.NetworkOperation)
                    {
                        if (command.OperationId == (int)NetworkOperationEnum.XhrStart)
                        {
                            if (XhrMonitor.CheckUntilAllXhrStartCallIsDone(_driver, command.Values[0]))
                            {
                                _testProgressEmitter.CommandComplete(testProgress);
                            }
                            else
                            {
                                testProgress.IsSuccesfull = false;
                                _testProgressEmitter.CommandComplete(testProgress);
                            }
                        }
                        else if (command.OperationId == (int)NetworkOperationEnum.XhrDone)
                        {
                            if (XhrMonitor.CheckUntilAllXhrDoneCallIsDone(_driver, command.Values[0]))
                            {
                                _testProgressEmitter.CommandComplete(testProgress);
                            }
                            else
                            {
                                testProgress.IsSuccesfull = false;
                                _testProgressEmitter.CommandComplete(testProgress);
                            }
                        }


                    }


                    if (command.WebDriverOperationType == WebDriverOperationType.BrowserOperation
                        && command.OperationId == (int)BrowserOperationEnum.GetScreenshot)
                    {
                        TakeScreenshot(commandMessage, command);
                    }
                    else
                    {

                        element = RunCommand(command);
                        if (command.OperationId == 18 && command.WebDriverOperationType == WebDriverOperationType.BrowserNavigationOperation)
                        {
                            //close browser is not send with status - if we want to do it add guid to close browser command
                            break;
                        }
                        testProgress.IsSuccesfull = true;
                        _testProgressEmitter.CommandComplete(testProgress);
                        var config = commandMessage.Configurations.FirstOrDefault(x => x.Id == 1);
                        if (config?.Value == "true")//Take Screenshot After Every Command
                        {
                            TakeScreenshot(commandMessage, command);
                        }
                    }


                }
                catch (Exception ex)
                {
                    // TODO - should it break when there is error ?????
                    testProgress.IsSuccesfull = false;
                    testProgress.Message = ex.Message;
                    _testProgressEmitter.CommandComplete(testProgress);
                }

            }
            // _driver.Close();
        }
        private void TakeScreenshot(CommandMessage commandMessage, SeleniumCommand command)
        {
            var screenshot = new BrowserOperation(_driver).GetScreenshot();
            ITestProgress testProgressImage = new ScreenshotTestProgress()
            {
                senderConnectionId = commandMessage.SenderConnectionId,
                command = command,
                IsSuccesfull = true,
                Screenshot = screenshot,
                TestRunHistoryId = commandMessage.TestRunHistoryId.Value
            };
            _testProgressEmitter.ScreenshotComplete(testProgressImage);
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
                case WebDriverOperationType.BrowserOperation:
                    new BrowserOperation(_driver).GetByEnum(command.OperationId, command.Values);
                    return null;
                case WebDriverOperationType.ElementOperation:
                    new ElementOperation(_driver).GetByEnum(command.OperationId, command.Values, element);
                    return null;
                case WebDriverOperationType.Locators:
                    return new Locator(_driver).GetByEnum(command.OperationId, command.Values);

                case WebDriverOperationType.BrowserNavigationOperation:
                    new BrowserNavigationOperation(_driver).GetByEnum(command.OperationId, command.Values);
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
