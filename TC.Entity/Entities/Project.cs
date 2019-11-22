using System;
using System.Collections.Generic;
using System.Text;

namespace TC.Entity.Entities
{
    public class Project : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual List<ProjectDomain> ProjectDomains { get; set; }
        public virtual List<UserInProject> UserInProject { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }
    }
}
