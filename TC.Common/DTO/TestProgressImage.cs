using System;
using System.Collections.Generic;
using System.Text;
using TC.Common.Selenium;

namespace TC.Common.DTO
{
    public class TestProgressImage
    {
        public string CommandTestGuid { get; set; }
        public string SenderConnectionId { get; set; }
        public bool IsSuccesful { get; set; }
        public string ImageBase64 { get; set; }
    }
}
