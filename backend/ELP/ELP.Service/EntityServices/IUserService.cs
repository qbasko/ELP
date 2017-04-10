using ELP.Model.Entities;
using ELP.Service.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ELP.Service
{
    public interface IUserService
    {
        Task<User> GetUserByUsername(string username);
        Task<IdentityResult> CreateUser(User user, string password);
        Task<SignInResult> SignIn(string username, string password);
        Task SignOut();
        PasswordVerificationResult VerifyHashedPassword(User user, string password);
        Task<IList<Claim>> GetClaims(User user);
    }
}
