using System.Collections.Generic;
using TC.BrowserEngine.Helpers.Enums;
using TC.Common.Selenium;

namespace TC.BrowserEngine.Controllers
{
    public class TestSprinter
    {
        private readonly BrowserType _browserType;
        public TestSprinter(BrowserType browserType)
        {
            _browserType = browserType;
        }
        public void Start(List<SeleniumCommand> list)
        {

            //  ChromeDriver driver = new BrowserDriver(_browserType).GetDriver() as ChromeDriver;
            // new CommandProcessor(driver).Start(list);
            //driver.Url = "http://www.google.com";

            //driver.ExecuteAsyncScript("alert('hey')");
            //Task.Delay(10000);

        }
    }
}
