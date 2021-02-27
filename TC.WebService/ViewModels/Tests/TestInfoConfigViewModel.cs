using TC.Entity.Entities;

namespace TC.WebService.ViewModels.Projects
{
    public class TestInfoConfigViewModel
    {
        public TestInfoConfigViewModel()
        {

        }
        
        public TestInfoConfigViewModel(TestInfoConfig testInfoConfig)
        {
            Id = testInfoConfig.Id;
            TestInfoId = testInfoConfig.TestInfo.Id;
            ConfigProjectTestId = testInfoConfig.ConfigProjectTestId;
            Value = testInfoConfig.Value;
        }
        
        public TestInfoConfigViewModel(ProjectTestConfig projectTestConfig, int testInfoId)
        {
            Id = 0;
            TestInfoId = testInfoId;
            ConfigProjectTestId = projectTestConfig.ConfigProjectTestId;
            Value = projectTestConfig.Value;
        }

        public TestInfoConfigViewModel(ConfigProjectTest configProjectTest, int testInfoId)
        {
            Id = 0;
            TestInfoId = testInfoId;
            ConfigProjectTestId = configProjectTest.Id;
            Value = configProjectTest.DefaultValue;
        }
        public int Id { get; set; }
        public int TestInfoId { get; set; }
        public int ConfigProjectTestId { get; set; }
        public string Value { get; set; }
    }

}
