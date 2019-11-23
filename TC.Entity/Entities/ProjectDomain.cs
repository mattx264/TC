﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TC.Entity.Entities
{
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