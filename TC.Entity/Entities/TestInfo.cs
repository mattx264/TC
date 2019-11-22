using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TC.Common.Selenium;

namespace TC.Entity.Entities
{
    public class TestInfo : IEntity
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
        public IList<SeleniumCommand> SeleniumCommands { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }
    }
}