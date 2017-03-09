using ELP.Model.Entities;
using ELP.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ELP.Model;

namespace ELP.Service
{
    public class UserService : EntityService<User>, IUserService
    {      
        public UserService(IContext context) : base(context)
        {
        }

        public User GetUserByUsername(string username)
        {
            return _dbset.FirstOrDefault(u => u.Username == username);
        }
    }
}
