using ELP.Model.Entities;
using ELP.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using ELP.Model;

namespace ELP.Service
{
    public class UserService : EntityService<User>, IUserService
    {
        IContext _context;

        public UserService(IContext context) : base(context)
        {
            _context = context;
            _dbset = _context.Set<User>();
        }

        public User GetUserByUsername(string username)
        {
            throw new NotImplementedException();
            //return _dbset.
        }
    }
}
