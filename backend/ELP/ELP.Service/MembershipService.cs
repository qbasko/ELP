using System;
using System.Collections.Generic;
using System.Text;
using ELP.Model.Entities;
using ELP.Model;
using ELP.Service.Exceptions;
using System.Linq;
using System.Security.Principal;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ELP.Service
{
    public class MembershipService : IMembershipService
    {
        private readonly IUserService _userService;
        private readonly IEncryptionService _encryptionService;
        private readonly IRoleService _roleService;
        private readonly IUserRoleService _userRoleService;

        public MembershipService(IUserService userService, IEncryptionService encryptionService, IRoleService roleService, IUserRoleService userRoleService)
        {
            _userService = userService;
            _encryptionService = encryptionService;
            _roleService = roleService;
            _userRoleService = userRoleService;
        }

        public User CreateUser(string username, string email, string password, ICollection<int> roles)
        {
            IdentityUser existingUser = _userService.GetUserByUsername(username);
            if (existingUser != null)
            {
                throw new UserAlreadyRegisteredException();
            }

            string passwordSalt = _encryptionService.CreateSalt();



            IdentityUser user = new IdentityUser()
            {                
                UserName = username,             
                Email = email,
                LockoutEnabled = false,
                PasswordHash = _encryptionService.EncryptPassword(password, passwordSalt),
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

            _userService.Create(user);

            foreach (int roleId in roles)
            {
                AddUserToRole(user, roleId);
            }

            return user;
        }

        public User GetUser(int userId)
        {
            throw new NotImplementedException();
        }

        public List<string> GetUserRoles(string username)
        {
            List<string> roles = new List<string>();
            IdentityUser user = _userService.GetUserByUsername(username);
            if (user != null)
            {
                foreach (var userRole in user.Roles)
                {
                    roles.Add(userRole.RoleId);
                }
            }
            return roles.Distinct().ToList();
        }

        public MembershipContext ValidateUser(string username, string password)
        {
            MembershipContext membershipCtx = new MembershipContext();
            IdentityUser user = _userService.GetUserByUsername(username);
            if (user != null && IsUserValid(user, password))
            {
                membershipCtx.User = user;
                List<string> userRoles = GetUserRoles(username);
                GenericIdentity identity = new GenericIdentity(user.UserName);
                membershipCtx.Principal = new GenericPrincipal(
                    identity,
                    userRoles.ToArray());
            }
            return membershipCtx;
        }

        private void AddUserToRole(User user, int roleId)
        {
            Role role = _roleService.GetRoleById(roleId);
            if (role == null)
            {
                throw new Exception("Role does not exist!");
            }

            var userRole = new UserRole()
            {
                RoleId = role.Id,
                UserId = user.Id,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

            _userRoleService.Create(userRole);
        }

        private bool IsPasswordValid(IdentityUser user, string password)
        {
            return true;
        }

        private bool IsUserValid(IdentityUser user, string password)
        {
            if (IsPasswordValid(user, password))
            {
                return !user.LockoutEnabled;
            }
            return false;
        }
    }
}
