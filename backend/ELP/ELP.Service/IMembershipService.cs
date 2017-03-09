using ELP.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELP.Service
{
    public interface IMembershipService
    {
        MembershipContext ValidateUser(string username, string password);
        User CreateUser(string username, string email, string password, ICollection<int> roles);
        User GetUser(int userId);
        List<Role> GetUserRoles(string username);
    }
}
