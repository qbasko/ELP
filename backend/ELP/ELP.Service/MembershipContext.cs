using ELP.Model.Entities;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace ELP.Service
{
    public class MembershipContext
    {
        public IPrincipal Principal { get; set; }
        public User User { get; set; }
        public bool IsValid()
        {
            return Principal != null;
        }
    }
}
