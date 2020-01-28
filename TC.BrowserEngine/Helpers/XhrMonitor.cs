using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace TC.BrowserEngine.Helpers
{
    class XhrMonitor
    {
        public static bool CheckUntilAllXhrsCallsAreDone(IWebDriver _driver)
        {
            var counter = 0;

            while(counter < 100)
            {
                var driverResponse = ((IJavaScriptExecutor)_driver).ExecuteScript(JavaScript.JavaScript.CheckIfAllXhrCallsDone()).ToString();

                if (driverResponse == "True")
                {
                    return true;
                }

                System.Threading.Thread.Sleep(100);
                counter++;
            }
            
            return false;
        }

        public static bool CheckUntilAllXhrCallIsDone(IWebDriver _driver, string xhrCall)
        {
            var counter = 0;
            var splittedXhrCall = xhrCall.Split(',')[0];

            while (counter < 100)
            {
                var driverResponse = ((IJavaScriptExecutor)_driver).ExecuteScript(JavaScript.JavaScript.CheckIfXhrCallDone(splittedXhrCall)).ToString();

                if (driverResponse == "True")
                {
                    return true;
                }

                System.Threading.Thread.Sleep(100);
                counter++;
            }

            return false;
        }
    }
}
