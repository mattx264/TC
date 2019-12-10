using System;
using System.Collections.Generic;
using System.Text;
using TC.Common.Selenium;

namespace TC.Common.DTO
{
    public class TestProgressMessage
    {
        public string SenderConnectionId { get; set; }
        public bool IsSuccesful { get; set; }

    }
}
