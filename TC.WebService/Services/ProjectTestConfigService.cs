using System.Collections.Generic;
using System.Linq;
using TC.DataAccess.Repositories;
using TC.DataAccess.Repositories.Interfaces;
using TC.WebService.Services.Interface;
using TC.WebService.ViewModels.Projects;

namespace TC.WebService.Services
{
    public class ProjectTestConfigService : IProjectTestConfigService
    {
        private IConfigProjectTestRepository _configProjectTestRepository;
        private ITestInfoConfigRepository _testInfoConfigRepository;
        private IProjectTestConfigRepository _projectTestConfigRepository;
        private ITestInfoRepository _testInfoRepository;

        public ProjectTestConfigService(
            IConfigProjectTestRepository configProjectTestRepository,
            ITestInfoConfigRepository testInfoConfigRepository,
            IProjectTestConfigRepository projectTestConfigRepository,
            ITestInfoRepository testInfoRepository
            )
        {
            _configProjectTestRepository = configProjectTestRepository;
            _testInfoConfigRepository = testInfoConfigRepository;
            _projectTestConfigRepository = projectTestConfigRepository;
            _testInfoRepository = testInfoRepository;
        }
        public IList<ProjectTestConfigViewModel> GetProjectConfigByTestId(int projectId)
        {

            var configs = _configProjectTestRepository.FindAll().ToList();           

            var projectTestConfigs = _projectTestConfigRepository.GetByProjectId(projectId);


            var result = new List<ProjectTestConfigViewModel>();
            foreach (var configProjectTest in configs)
            {
                var projectTestConfig = projectTestConfigs.FirstOrDefault(x => x.ConfigProjectTestId == configProjectTest.Id);
                if (projectTestConfig == null)
                {
                    result.Add(new ProjectTestConfigViewModel().Convert(0, projectId, configProjectTest));
                }
                else
                {
                    result.Add(new ProjectTestConfigViewModel().Convert(projectTestConfig));
                }
            }
            return result;

        }

        public IList<TestInfoConfigViewModel> GetTestConfigByTestId(int testInfoId)
        {
            var testInfo = _testInfoRepository.FindById(testInfoId);

            var configs = _configProjectTestRepository.FindAll().ToList();
            var testInfoConfig = _testInfoConfigRepository.FindByTestId(testInfoId);
            var result = new List<TestInfoConfigViewModel>();
            if (testInfoConfig != null && testInfoConfig.Count == configs.Count)
            {
                result = testInfoConfig.Select(x => new TestInfoConfigViewModel(x)).ToList();
            }

            var projectTestConfigs = _projectTestConfigRepository.GetByProjectId(testInfo.ProjectId);

            foreach (var configProjectTest in configs)
            {
                var projectTestConfig = projectTestConfigs.FirstOrDefault(x => x.ConfigProjectTestId == configProjectTest.Id);
                if (projectTestConfig == null)
                {
                    result.Add(new TestInfoConfigViewModel(configProjectTest, testInfoId));
                }
                else
                {
                    result.Add(new TestInfoConfigViewModel(projectTestConfig, testInfoId));
                }
            }
            return result;
        }
    }
}
