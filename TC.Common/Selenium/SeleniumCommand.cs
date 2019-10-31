using System;
using System.Collections.Generic;
using System.Text;
using TC.Common.Selenium.WebDriverOperation;

namespace TC.Common.Selenium
{
    public class SeleniumCommand
    {
        public int OperationId { get; set; }
        public WebDriverOperationType WebDriverOperationType { get; set; }
        public string[] Values { get; set; }
    }
}
