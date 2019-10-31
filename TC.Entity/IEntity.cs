using System;
using System.Collections.Generic;
using System.Text;

namespace TC.Entity
{
    public interface IEntity
    {
        int Id { get; set; }
        bool IsActive { get; set; }
        string CreatedBy { get; set; }
        string ModifiedBy { get; set; }
        DateTime DateAdded { get; set; }
        DateTime DateModified { get; set; }
    }

}
