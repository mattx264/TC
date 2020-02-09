using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TC.Common.Selenium;

namespace TC.WebService.ViewModels
{
    public class TestInfoViewModel
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<SeleniumCommand> Commands { get; set; }
    }
}
