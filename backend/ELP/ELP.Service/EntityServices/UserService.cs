using ELP.Model.Entities;
using ELP.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ELP.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace ELP.Service
{
    public class UserService : EntityService<IdentityUser>, IUserService
    {
        private UserManager<IdentityUser> _userManager;

        public UserService(IContext context, UserManager<IdentityUser> userManager) : base(context)
        {            
            _userManager = userManager;
            //var userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(context));
        }

        public IdentityUser GetUserByUsername(string username)
        {
            return _dbset.FirstOrDefault(u => u.UserName == username);
        }

        public async Task<IdentityResult> CreateUser(IdentityUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }


    }
}
