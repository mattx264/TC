using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace TC.BrowserEngine.Helpers
{
    public class XhrMonitor
    {
        public static bool CheckUntilAllXhrsCallsAreDone(IWebDriver _driver)
        {
            var counter = 0;

            while(counter < 100)
            {
                var js = JavaScript.JavaScript.CheckIfAllXhrCallsDone();
                var driverResponse = ((IJavaScriptExecutor)_driver).ExecuteScript(js).ToString();

                if (driverResponse == "True")
                {
                    return true;
                }

                System.Threading.Thread.Sleep(100);
                counter++;
            }
            
            return false;
        }

        public static bool CheckUntilAllXhrStartCallIsDone(IWebDriver _driver, string xhrCall)
        {
            var counter = 0;
            var splittedXhrCall = xhrCall.Split(',')[0];

            while (counter < 100)
            {
                var driverResponse = ((IJavaScriptExecutor)_driver).ExecuteScript(JavaScript.JavaScript.CheckIfXhrStartCallIsDone(splittedXhrCall)).ToString();

                if (driverResponse == "True")
                {
                    return true;
                }

                System.Threading.Thread.Sleep(100);
                counter++;
            }

            return false;
        }

        public static bool CheckUntilAllXhrDoneCallIsDone(IWebDriver _driver, string xhrCall)
        {
            var counter = 0;
            var splittedXhrCall = xhrCall.Split(',')[0];

            while (counter < 100)
            {
                var driverResponse = ((IJavaScriptExecutor)_driver).ExecuteScript(JavaScript.JavaScript.CheckIfXhrDoneCallIsDone(splittedXhrCall)).ToString();

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
