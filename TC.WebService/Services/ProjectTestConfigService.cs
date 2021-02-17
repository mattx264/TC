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
        public IList<ProjectTestConfigViewModel> GetByTestId(int testId)
        {
            var testInfo = _testInfoRepository.FindById(testId);

            var configs = _configProjectTestRepository.FindAll().ToList();
            var testInfoConfig = _testInfoConfigRepository.FindByTestId(testId);

            if (testInfoConfig != null && testInfoConfig.Count == configs.Count)
            {
                var testInfoConfigViewMode = testInfoConfig.Select(x => new TestInfoConfigViewModel(x));
            }

            var projectTestConfigs = _projectTestConfigRepository.GetByProjectId(testInfo.ProjectId);


            var result = new List<ProjectTestConfigViewModel>();
            foreach (var configProjectTest in configs)
            {
                var projectTestConfig = projectTestConfigs.FirstOrDefault(x => x.ConfigProjectTestId == configProjectTest.Id);
                if (projectTestConfig == null)
                {
                    result.Add(new ProjectTestConfigViewModel().Convert(0, testInfo.ProjectId, configProjectTest));
                }
                else
                {
                    result.Add(new ProjectTestConfigViewModel().Convert(projectTestConfig));
                }
            }
            return result;

            //var projectId = _testInfoRepository.FindById(testId).ProjectId;
            //var projectTestConfigs = _projectTestConfigRepository.GetByProjectId(projectId);
            //if (projectTestConfigs != null && projectTestConfigs.Count > 0)
            //{
            //    var configProjectTestViewModel = projectTestConfigs.Select(x => new ProjectTestConfigViewModel().Convert(x));
            //    return Ok(configProjectTestViewModel);
            //}
        }
    }
}
