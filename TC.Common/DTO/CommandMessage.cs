using System;
using System.Collections.Generic;
using System.Text;
using TC.Common.Selenium;

namespace TC.Common.DTO
{
    class CommandMessage
    {
        public string SenderConnectionId { get; set; }
        public List<SeleniumCommand> Commands { get; set; }
       
    }
}
