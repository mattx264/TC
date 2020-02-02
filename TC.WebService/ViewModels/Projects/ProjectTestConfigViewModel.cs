using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TC.Entity.Entities;

namespace TC.WebService.ViewModels.Projects
{
    public class ProjectTestConfigViewModel
    {
        public ProjectTestConfigViewModel(ProjectTestConfig projectTestConfig)
        {
            Id = projectTestConfig.Id;
            ProjectId = projectTestConfig.ProjectId;
            ConfigProjectTestId = projectTestConfig.ConfigProjectTestId;
            Value = projectTestConfig.Value;
        }
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int ConfigProjectTestId { get; set; }
        public string Value { get; set; }
    }
}
