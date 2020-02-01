using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TC.Entity.Entities;
using static TC.Entity.Entities.ConfigProjectTest;

namespace TC.WebService.ViewModels
{
    public class ConfigProjectTestViewModel
    {
        public ConfigProjectTestViewModel(ConfigProjectTest configProjectTest)
        {
            Id = configProjectTest.Id;
            Name = configProjectTest.Name;
            Description = configProjectTest.Description;
            Type = configProjectTest.Type;
            DefaultValue = configProjectTest.DefaultValue;
            IsActive = configProjectTest.IsActive;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ConfigProjectTestEnum Type { get; set; }
        public string DefaultValue { get; set; }
        public bool IsActive { get; set; }
    }
}
