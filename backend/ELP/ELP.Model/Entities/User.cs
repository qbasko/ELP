using ELP.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELP.Model.Entities
{
    public class User : AuditableEntity<int>
    {
        public User()
        {
            UserRoles = new List<UserRole>();
        }

        public string Username { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public string Salt { get; set; }
        public bool IsLocked { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
