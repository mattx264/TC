using System;
using System.Collections.Generic;
using System.Text;

namespace TC.Common.Selenium
{
    //https://www.automatetheplanet.com/selenium-webdriver-csharp-cheat-sheet/
    public enum BrowserOperationEnum 
    {
        WebDriverWait,
        WaitUntil,
        WaitUntilBrowserReady,
        GoToUrl,
        Title,
        Url,
        PageSource,
        WindowHandles,
        Back,
        Refresh,
        Forward,
        Scroll,
        Maximize,
        AddCookie,
        AllCookies,
        DeleteCookieNamed,
        DeleteAllCookies,
        GetScreenshot,
        CloseBrowser
    }
}
