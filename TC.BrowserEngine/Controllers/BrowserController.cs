using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using TC.BrowserEngine.Helpers.Enums;
using TC.BrowserEngine.Selenium;
using TC.Common.DTO;

namespace TC.BrowserEngine.Controllers
{
    public interface IBrowserController
    {
        public void Start();
        public void Setup(BrowserType browserType, CommandMessage commandMessage);
        public void RunCommandProcessor();
    }
    public class BrowserController : IBrowserController
    {
        private BrowserType _browserType;
        private IWebDriver driver;
        private CommandProcessor _commandProcessor;
        private CommandMessage _commandMessage;
        public BrowserController()
        {

        }
        public void Setup(BrowserType browserType, CommandMessage commandMessage)
        {
            _browserType = browserType;
            _commandMessage = commandMessage;
        }
        public void Start()
        {
            if (_commandMessage == null)
            {
                throw new Exception("Browser controller has to be Setup before call Start method");
            }
            driver = new BrowserDriver(_browserType).GetDriver() as ChromeDriver;
            _commandProcessor = new CommandProcessor(driver);
            //driver.Url = "http://www.google.com";
            //driver.ExecuteAsyncScript("alert('hey')");
            //Task.Delay(10000);
        }
        public void RunCommandProcessor()
        {
            if (_commandMessage == null)
            {
                throw new Exception("Browser controller has to be Setup before call RunCommandProcessor method");
            }
            if (_commandProcessor == null)
            {
                throw new Exception("Browser controller has to be Start before call RunCommandProcessor method");
            }

            _commandProcessor.Start(_commandMessage);
            driver.Quit();

        }
        //public void ExecCommand(List<SeleniumCommand> list)
        //{
        //    _commandProcessor.Exec(list);

        //}
        //public void ExecSingleCommand(SeleniumCommand command)
        //{
        //    List<SeleniumCommand> list = new List<SeleniumCommand>() { command };
        //    _commandProcessor.Exec(list);

        //}
        public string GetPageSource()
        {
            return _commandProcessor.GetPageSource();
        }
        public bool IsBrowserRunning()
        {
            try
            {
                var count = driver.WindowHandles.Count;
                return count > 0 ? true : false;
            }catch(Exception ex)
            {
                return false;
            }
        }

    }



}
