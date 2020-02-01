using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TC.Common.Selenium;

namespace TC.Entity.Entities
{
    [Table("TestInfoConfig", Schema = "test")]
    public class TestInfoConfig : IEntity
    {
        public int Id { get; set; }
        public virtual TestInfo Project { get; set; }
        public int TestInfoId { get; set; }
        public int ConfigProjectTestId { get; set; }
        public virtual ConfigProjectTest ConfigProjectTest { get; set; }
        public string Value { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }
    }
}