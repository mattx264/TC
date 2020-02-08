using System;
using System.Collections.Generic;
using System.Text;

namespace TC.Common.DTO
{
    public class ConfigurationModel
    {
        public int Id { get; set; }
        public int ConfigProjectTestId { get; set; }
        public int ProjectId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string value { get; set; }
    }
}
