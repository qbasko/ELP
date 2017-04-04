using ELP.Model.Entities;
using ELP.Service.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ELP.Service
{
    public interface IUserService: IEntityService<IdentityUser>
    {
        IdentityUser GetUserByUsername(string username);
        Task<IdentityResult> CreateUser(IdentityUser user, string password);
    }
}
