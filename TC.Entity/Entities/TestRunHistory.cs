﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TC.Entity.Entities
{
    public class TestRunHistory : IEntity
    {
        public int Id { get; set; }
        public virtual TestInfo TestInfo { get; set; }
        public int TestInfoId { get; set; }
        public virtual IList<TestRunResult> TestRunResults { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }
        
    }
}