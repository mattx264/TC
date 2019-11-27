using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TC.Entity.Entities
{
    public class UserModel : IdentityUser, IEntity
    {
        [Key]
        public new int Id { get; set; }
        public Guid Guid { get; set; }
        public virtual List<UserInGroup> UserGroups { get; set; }
        public override string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }
    }
}
