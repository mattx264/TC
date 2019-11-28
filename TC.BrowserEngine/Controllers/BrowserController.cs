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
    public interface IBrowserController
    {
        public void Start();
        public void Setup(BrowserType browserType, List<SeleniumCommand> commands);
        public void RunCommandProcessor();
    }
    public class BrowserController : IBrowserController
    {
        private BrowserType _browserType;
        private IWebDriver driver;
        private CommandProcessor _commandProcessor;
        private List<SeleniumCommand> _commands;
        public BrowserController()
        {

        }
        public void Setup(BrowserType browserType, List<SeleniumCommand> commands)
        {
            _browserType = browserType;
            _commands = commands;
        }
        public void Start()
        {
            if(_commands == null)
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
            // should be always new instance or not ???
            if (_commands == null )
            {
                throw new Exception("Browser controller has to be Setup before call RunCommandProcessor method");
            }
            if (_commandProcessor == null)
            {
                throw new Exception("Browser controller has to be Start before call RunCommandProcessor method");
            }
            _commandProcessor.Start(_commands);

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
      
    }
}
