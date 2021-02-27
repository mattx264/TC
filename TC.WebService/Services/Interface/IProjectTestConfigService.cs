using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TC.WebService.ViewModels.Projects;

namespace TC.WebService.Services.Interface
{
    public interface IProjectTestConfigService
    {
        public IList<ProjectTestConfigViewModel> GetProjectConfigByTestId(int projectId);
        public IList<TestInfoConfigViewModel> GetTestConfigByTestId(int testId);
    }
}
