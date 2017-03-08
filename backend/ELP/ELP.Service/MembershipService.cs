using System;
using System.Collections.Generic;
using System.Text;
using ELP.Model.Entities;
using ELP.Model;

namespace ELP.Service
{
    public class MembershipService : IMembershipService
    {
        private readonly IContext _context;
        private readonly IUserService _userService;
        private readonly IEncryptionService _encryptionService;

        public MembershipService(IUserService userService, IEncryptionService encryptionService)
        {
            _userService = userService;
            _encryptionService = encryptionService;
        }

        public User CreateUser(string username, string email, string password, ICollection<int> roles)
        {
            
            User existingUser = _userService.GetUserByUsername(username);
            if(existingUser!=null)
            {
                throw new Exception("Username is already registered");
            }

            string passwordSalt = _encryptionService.CreateSalt();

            User user = new User()
            {
                Username = username,
                Salt = passwordSalt,
                Email = email,
                IsLocked = false,
                HashedPassword = _encryptionService.EncryptPassword(password, passwordSalt),
                CreatedDate = DateTime.Now
            };

            _userService.Create(user);
            
            //TODO Add roles
            
            return null;
            
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
