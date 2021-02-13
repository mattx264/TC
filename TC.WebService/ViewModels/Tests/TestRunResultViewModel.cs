using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TC.Common.Selenium;
using TC.Common.Selenium.WebDriverOperation;
using TC.Entity.Entities;

namespace TC.WebService.ViewModels.Tests
{
    public class TestRunResultViewModel
    {
        public TestRunResultViewModel(TestRunResult testRunResult,TestInfo testInfo)
        {
            SeleniumCommand = testInfo.SeleniumCommands.FirstOrDefault(x => x.Guid == testRunResult.CommandTestGuid);
            CommandTestGuid = testRunResult.CommandTestGuid;
            RunTime = testRunResult.RunTime;
            ScreenshotUrl = testRunResult?.Screenshot != null ? testRunResult?.Screenshot?.Path:null;
            IsSuccesful = testRunResult.IsSuccesful;
            CreatedBy = testRunResult.CreatedBy;
            DateAdded = testRunResult.DateAdded;

        }

        public SeleniumCommand SeleniumCommand { get; }
        public string CommandTestGuid { get; set; }
        public int RunTime { get; set; }
        public string ScreenshotUrl { get; set; }
        public bool IsSuccesful { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateAdded { get; set; }      
    }
}
