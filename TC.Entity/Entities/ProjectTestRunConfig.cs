using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TC.Entity.Entities
{
    [Table("ProjectTestRunConfig", Schema = "project")]
    public class ProjectTestRunConfig : IEntity
    {
        public int Id { get; set; }
        public virtual Project Project { get; set; }
        public int ProjectId { get; set; }
        public int TestRunConfigId { get; set; }
        public virtual TestRunConfig TestRunConfig { get; set; }
        public string Value { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }
    }
}
