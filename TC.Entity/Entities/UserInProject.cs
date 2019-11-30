using System;
using System.Collections.Generic;
using System.Text;
using TC.Entity.Entities.User;

namespace TC.Entity.Entities
{
    public class UserInProject : IEntity
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int UserModelId { get; set; }
        public int UserProjectStatusId { get; set; }
        public virtual Project Project { get; set; }
        public virtual UserModel UserModel { get; set; }
        public virtual UserProjectStatus UserProjectStatus { get; set; }

        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }
    }
}
