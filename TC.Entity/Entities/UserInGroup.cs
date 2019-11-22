using System;
using System.Collections.Generic;
using System.Text;

namespace TC.Entity.Entities
{
    public class UserInGroup : IEntity
    {
        public int Id { get; set; }
        public int UserModelId { get; set; }
        public virtual UserGroup UserGroup { get; set; }
        public int UserGroupId { get; set; }
        public virtual UserModel UserModel { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }
    }
}
