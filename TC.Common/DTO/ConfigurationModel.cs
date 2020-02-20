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
        public string Name { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
    }
}
