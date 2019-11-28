using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TC.BrowserEngine.Helpers.Enums;
using TC.BrowserEngine.HtmlAgility;
using TC.Common.Selenium;
using TC.Common.Selenium.WebDriverOperation;

namespace TC.BrowserEngine.Controllers
{
    public class TestGeneratorController
    {
        private BrowserController _browserController;

        public TestGeneratorController()
        {
            _browserController = new BrowserController();
            _browserController.Start();
        }
        public void StartGenerator(string startUrl)
        {

            // FIRST TYPE OF APPLICATION 
            // UES and IAR is flow application - fill out data and you going from page to page, finale page is end of application - there is one (maybe two start points e.g. start from create new application or login and edit)
            // RAMP and intranet are one page action or more -> 
            /*
             * Goto page and collect page source code
             *  FIRST STEP IS FIND ALL POSSIBLE ROUTING -LINKS
             * send code to process 
             * process find all navigation element
             * send possible navigation to szwagier
             * szwagier is try first element from prosess 
             *      -> link not working (is button and make some javascript action)
             *      -> link route to new url -szwagier mark link as active and going back to source page
             *      ->
             *      
             *  SECOUND STEP IS TO FIND ALL POSSIBLE ROUTING - MORE ADVICE -
             */

            /**
             * 
             * IDEA IS TO CREATE SMOKE TEST WITH ALL POSIBLE ACTIONS ON PAGE - main resome to generate this test is make sure server will able to handle crazy user action
             */
            /*
              *GO to url and get all possible actions e.g. UES landing page will have start new app button user name, password, submit button (exluding header and footer). 
              * now user can selected what action eg add user name and password and click submit now wait to new page loaded - than repet getting possible actions.
              * 
              */
             
            //_browserController.ExecSingleCommand(new SeleniumCommand() {
            //    OperationId = (int)BrowserOperationEnum.GoToUrl,
            //    Values =new string[] { startUrl },
            //    WebDriverOperationType = WebDriverOperationType.BrowserNavigationOperation
            //});
            //string html=_browserController.GetPageSource();
            //TestGenerator testGenerator = new TestGenerator();
            //testGenerator.LoadHtml(html);
            //var elements=testGenerator.CollectPossibleRoutingActions();
            //foreach(var ele in elements)
            //{
            //    int status=_browserController.ClickElement(ele.XPath);
            //    if(status == 0)
            //    {
            //        //get current url and go back
            //        //save element and url in websiteMap
            //    }
            //}
        }
    }
}
