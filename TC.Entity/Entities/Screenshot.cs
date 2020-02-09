using System;
using System.Collections.Generic;
using System.Text;

namespace TC.Entity.Entities
{
   public class Screenshot : IEntity
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }
    }
}
