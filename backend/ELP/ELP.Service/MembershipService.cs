using System;
using System.Collections.Generic;
using System.Text;
using ELP.Model.Entities;
using ELP.Model;

namespace ELP.Service
{
    public class MembershipService : IMembershipService
    {
        private IContext _context;

        public MembershipService(IContext context)
        {
            _context = context;
        }

        public User CreateUser(string username, string email, string password, ICollection<int> roles)
        {
           User existingUser = 
        }

        public User GetUser(int userId)
        {
            throw new NotImplementedException();
        }

        public List<Role> GetUserRoles(string username)
        {
            throw new NotImplementedException();
        }

        public MembershipService ValidateUser(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
