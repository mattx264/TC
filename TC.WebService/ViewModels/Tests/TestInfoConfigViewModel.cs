using TC.Entity.Entities;

namespace TC.WebService.ViewModels.Projects
{
    public class TestInfoConfigViewModel
    {
        public TestInfoConfigViewModel(TestInfoConfig testInfoConfig)
        {
            Id = testInfoConfig.Id;
            TestInfoId = testInfoConfig.TestInfo.Id;
            ConfigProjectTestId = testInfoConfig.ConfigProjectTestId;
            Value = testInfoConfig.Value;
        }
        public int Id { get; set; }
        public int TestInfoId { get; set; }
        public int ConfigProjectTestId { get; set; }
        public string Value { get; set; }
    }

}
