using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TC.Entity.Entities
{
    public class ConfigProjectTest : IEntity
    {
        public enum ConfigProjectTestEnum
        {
            Boolean,
            String,
            List
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ConfigProjectTestEnum Type { get; set; }
        public string DefaultValue { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }
    }
}
