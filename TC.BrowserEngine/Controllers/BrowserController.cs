using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TC.BrowserEngine.Helpers.Enums;
using TC.BrowserEngine.Selenium;
using TC.Common.Selenium;
using TC.Common.Selenium.WebDriverOperation;

namespace TC.BrowserEngine.Controllers
{
    public class BrowserController
    {
        private readonly BrowserType _browserType;
        private IWebDriver driver;
        private CommandProcessor _commandProcessor;
        public BrowserController(BrowserType browserType) 
        {
            _browserType = browserType;
        }
        public void Start()
        {
            driver = new BrowserDriver(_browserType).GetDriver() as ChromeDriver;
            _commandProcessor = new CommandProcessor(driver);
            //driver.Url = "http://www.google.com";
            //driver.ExecuteAsyncScript("alert('hey')");
            //Task.Delay(10000);
        }
        public void RunCommandProcessor(List<SeleniumCommand> list)
        {
            // should be always new instance or not ???
            new CommandProcessor(driver).Start(list);

        }
        public void ExecCommand(List<SeleniumCommand> list)
        {
            _commandProcessor.Exec(list);

        }
        public void ExecSingleCommand(SeleniumCommand command)
        {
            List<SeleniumCommand> list = new List<SeleniumCommand>() { command };
            _commandProcessor.Exec(list);

        }
        public string GetPageSource()
        {
            return _commandProcessor.GetPageSource();
        }
        public int ClickElement(string xPath)
        {
            try
            {
                ExecCommand(new List<SeleniumCommand>() {
            new SeleniumCommand{
                WebDriverOperationType = WebDriverOperationType.Locators,
                OperationId = (int)LocatorsEnum.ByXPath,
                Values= new string[]{xPath }
            }, new SeleniumCommand{
                WebDriverOperationType = WebDriverOperationType.ElementOperation,
                OperationId = (int)ElementOperationEnum.Click
            }});
                return 0;
            }
            catch (Exception ex)
            {
                if (!ex.Message.Contains("element not interactable"))
                {
                    throw ex;
                }
                return -1;
            }
        }
    }
}
