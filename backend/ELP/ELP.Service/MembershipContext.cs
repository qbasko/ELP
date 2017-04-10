using ELP.Model.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace ELP.Service
{
    public class MembershipContext
    {
        public IPrincipal Principal { get; set; }
        public IdentityUser User { get; set; }
        public bool IsValid()
        {
            return Principal != null;
        }
    }
}
