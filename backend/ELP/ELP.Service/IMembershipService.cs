using ELP.Model.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELP.Service
{
    public interface IMembershipService
    {
        MembershipContext ValidateUser(string username, string password);
        IdentityUser CreateUser(string username, string email, string password, ICollection<int> roles);
        IdentityUser GetUser(int userId);
        List<string> GetUserRoles(string username);
    }
}
