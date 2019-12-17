using System;
using System.Collections.Generic;
using System.Text;

namespace TC.Entity.Entities
{

    public class TestRunConfig : IEntity
    {
        public enum TestRunConfigType
        {
            Boolean,
            String,
            List
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TestRunConfigType Type { get; set; }
        public string Value { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }
    }
}
