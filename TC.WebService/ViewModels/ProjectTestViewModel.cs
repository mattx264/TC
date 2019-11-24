using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TC.Common.Selenium;

namespace TC.WebService.ViewModels
{
    public class ProjectTestViewModel
    {
        public IList<SeleniumCommand> SeleniumCommands { get; set; }
        public int ProjectId { get; set; }
    }
}
