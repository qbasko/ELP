using System;
using System.Collections.Generic;
using System.Text;
using ELP.Model.Entities;
using ELP.Model;
using ELP.Service.Exceptions;

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

            User existingUser = _userService.GetUserByUsername(username);
            if (existingUser != null)
            {
                throw new UserAlreadyRegisteredException();
            }

            string passwordSalt = _encryptionService.CreateSalt();

            User user = new User()
            {
                Id = 1,
                Username = username,
                Salt = passwordSalt,
                Email = email,
                IsLocked = false,
                HashedPassword = _encryptionService.EncryptPassword(password, passwordSalt),
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

        public List<Role> GetUserRoles(string username)
        {
            throw new NotImplementedException();
        }

        public MembershipService ValidateUser(string username, string password)
        {
            throw new NotImplementedException();
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

    }
}
