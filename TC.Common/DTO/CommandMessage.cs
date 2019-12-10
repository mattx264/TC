﻿using System;
using System.Collections.Generic;
using System.Text;
using TC.Common.Selenium;

namespace TC.Common.DTO
{
    public class CommandMessage
    {
        public string  ReceiverConnectionId { get; set; }
        public string SenderConnectionId { get; set; }
        public List<SeleniumCommand> Commands { get; set; }
       
    }
}