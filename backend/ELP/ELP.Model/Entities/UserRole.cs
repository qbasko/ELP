using ELP.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELP.Model.Entities
{
    public class UserRole: AuditableEntity<int>
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
