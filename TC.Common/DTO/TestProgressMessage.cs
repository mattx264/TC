using System;
using System.Collections.Generic;
using System.Text;
using TC.Common.Selenium;

namespace TC.Common.DTO
{
    public class TestProgressMessage
    {
        public string CommandTestGuid { get; set; }
        public string SenderConnectionId { get; set; }
        public bool IsSuccesful { get; set; }
        public int TestRunHistoryId { get; set; }
        public string Message { get; set; }
    }
}
