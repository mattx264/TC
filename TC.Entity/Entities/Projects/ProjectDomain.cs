using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TC.Entity.Entities.Projects;

namespace TC.Entity.Entities
{
    [Table("ProjectDomain",Schema = "project")]
    public class ProjectDomain : IEntity
    {
        public int Id { get; set; }
        public string Domain { get; set; }
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }      
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }
    }
}
