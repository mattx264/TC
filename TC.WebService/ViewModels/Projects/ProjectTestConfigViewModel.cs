using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TC.Entity.Entities;

namespace TC.WebService.ViewModels.Projects
{
    public class ProjectTestConfigViewModel
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int ConfigProjectTestId { get; set; }
        public string Value { get; set; }

        public ProjectTestConfigViewModel Convert(int id, int projectId, ConfigProjectTest configProjectTest)
        {
            Id = id;
            ProjectId = projectId;
            ConfigProjectTestId = configProjectTest.Id;
            Value = configProjectTest.DefaultValue;
            return this;
        }
        public ProjectTestConfigViewModel Convert(ProjectTestConfig projectTestConfig)
        {
            Id = projectTestConfig.Id;
            ProjectId = projectTestConfig.ProjectId;
            ConfigProjectTestId = projectTestConfig.ConfigProjectTestId;
            Value = projectTestConfig.Value;
            return this;
        }
    }
}
