using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TC.Entity.Entities;

namespace TC.WebService.ViewModels.Tests
{
    public class TestRunHistoryViewModel
    {
        public TestRunHistoryViewModel(TestRunHistory testRunHistory)
        {
            Id = testRunHistory.Id;
            TestInfoId = testRunHistory.TestInfoId;
            TestInfoName = testRunHistory.TestInfo.Name;
            CreatedBy = testRunHistory.CreatedBy;
            DateAdded = testRunHistory.DateAdded;
            IsSuccesful = testRunHistory.TestRunResults.Count == 0 ?false : testRunHistory.TestRunResults.Last().IsSuccesful;
        }
        public int Id { get; set; }
        public int TestInfoId { get; set; }
        public string TestInfoName { get; set; }
        public bool IsSuccesful { get; set; }
        public string CreatedBy { get; set; }   
        public DateTime DateAdded { get; set; }
    }
}
