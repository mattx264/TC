﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TC.BrowserEngine.Controllers;
using TC.BrowserEngine.Helpers.Enums;
using TC.Common.Selenium;
using TC.Common.Selenium.WebDriverOperation;

namespace TC.BrowserEngineTest.Signal
{
    public class BrowserControllerTest
    {
        [Test]
        public async System.Threading.Tasks.Task OpenBrowserAsync()
        {
            var browser = new BrowserController(BrowserType.Chrome);
            await browser.Start();
            browser.ExecCommand(new List<SeleniumCommand> {
                new SeleniumCommand()
                {
                    WebDriverOperationType=WebDriverOperationType.BrowserNavigationOperation,
                    OperationId=3,
                    Values=new string[]{ "https://www.google.com/" }
            } });
        
        }
    }
}
