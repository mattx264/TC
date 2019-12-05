using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TC.Common.Selenium.WebDriverOperation;

namespace TC.Common.Selenium
{
    public class SeleniumCommand
    {
        public int OperationId { get; set; }
        public WebDriverOperationType WebDriverOperationType { get; set; }
        [NotMapped]
        public string[] Values { get; set; }

        public string Guid { get; set; }
    }
}
