﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TC.Entity.Entities
{
    public class TestRunResult : IEntity
    {
        public int Id { get; set; }
        public string CommandTestGuid { get; set; }
        public int RunTime { get; set; }
        public virtual Screenshot Screenshot { get; set; }
        public int ScreenshotId { get; set; }
        public bool IsSuccesful { get; set; } 
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }
    }
}
