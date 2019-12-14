using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Text;
using TC.BrowserEngine.Helpers;
using TC.BrowserEngine.Helpers.Enums;

namespace TC.BrowserEngine.Selenium
{
    public class BrowserDriver
    {
        private readonly BrowserType _browserType;
        private readonly string _browserVersion;
        private readonly IWebDriver _driver;
     

        public BrowserDriver(BrowserType browserType)
        {
            _browserType = browserType;
            var path = FileHelper.GetRootPath();

            if (browserType == BrowserType.Chrome)
            {
                var proxy = new Proxy
                {
                    HttpProxy = "http://localhost:18882",
                    SslProxy = "http://localhost:18882",
                    FtpProxy = "http://localhost:18882"
                };
                var options = new ChromeOptions
                {
                    Proxy = proxy
                };
                //TODO get dynamic way to get version wmic datafile where name="C:\\Program Files (x86)\\Google\\Chrome\\Application\\chrome.exe" get Version /value
                _browserVersion = "78";
                _driver = new ChromeDriver($"{path}/BrowserDrivers/Chrome/{_browserVersion}",new ChromeOptions() { 
                Proxy= proxy
                });
            } else if(browserType== BrowserType.Firefox)
            {
                _browserVersion = "0.24";
                _driver = new FirefoxDriver($"{path}/BrowserDrivers/Firefox/{_browserVersion}");
            }

        }    
        public IWebDriver GetDriver()
        {
            return _driver;
        }
    }
}
