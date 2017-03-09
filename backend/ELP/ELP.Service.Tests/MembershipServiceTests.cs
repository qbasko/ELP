using ELP.Model.Entities;
using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using ELP.Model;
using ELP.Service.Tests.Common;
using System.Linq;
using ELP.Service.Exceptions;

namespace ELP.Service.Tests
{
    public class MembershipServiceTests
    {


        [Fact]
        public void CreateUserTestFail_userAlreadyRegistered()
        {
            string username = "testUser1";
            var ctx = new Mock<IContext>();
            List<User> users = new List<User>();
            users.Add(new User() { Username = username });
            List<Role> roles = new List<Role>();
            roles.Add(new Role() { Id = 1, Name = "Role1" });
            roles.Add(new Role() { Id = 2, Name = "Role2" });

            var usersMockDbSet = ServiceTestsHelper.GetMockDbSet<User>(users);
            var rolesMockDbSet = ServiceTestsHelper.GetMockDbSet<Role>(roles);

            ctx.Setup(c => c.Set<User>()).Returns(usersMockDbSet.Object);
            ctx.Setup(c => c.Set<Role>()).Returns(rolesMockDbSet.Object);
            IUserService userService = new UserService(ctx.Object);
            IRoleService roleService = new RoleService(ctx.Object);
            IUserRoleService userRoleService = new UserRoleService(ctx.Object);
            var encryptionService = new EncryptionService();
            IMembershipService service = new MembershipService(userService, encryptionService, roleService, userRoleService);

            Assert.ThrowsAny<UserAlreadyRegisteredException>(() =>
            {
                service.CreateUser("testUser1", "test@email.com", "password", new List<int>() { 1, 2 });
            });
        }

        [Fact]
        public void CreateUserTest()
        {
            var ctx = new Mock<IContext>();
            List<Role> roles = new List<Role>();
            roles.Add(new Role() { Id = 1, Name = "Role1" });
            roles.Add(new Role() { Id = 2, Name = "Role2" });

            var rolesMockDbSet = ServiceTestsHelper.GetMockDbSet<Role>(roles);
            var userRolesMockDbSet = ServiceTestsHelper.GetMockDbSet<UserRole>(new List<UserRole>());
            var usersMockDbSet = ServiceTestsHelper.GetMockDbSet<User>(new List<User>());

            ctx.Setup(c => c.Set<Role>()).Returns(rolesMockDbSet.Object);
            ctx.Setup(c => c.Set<User>()).Returns(usersMockDbSet.Object);
            ctx.Setup(c => c.Set<UserRole>()).Returns(userRolesMockDbSet.Object);

            IUserService userService = new UserService(ctx.Object);
            IRoleService roleService = new RoleService(ctx.Object);
            IUserRoleService userRoleService = new UserRoleService(ctx.Object);

            var encryptionService = new EncryptionService();
            IMembershipService service = new MembershipService(userService, encryptionService, roleService, userRoleService);
            User result = service.CreateUser("testUser1", "test@email.com", "password", new List<int>() { 1, 2 });

            usersMockDbSet.Verify(m => m.Add(It.IsAny<User>()), Times.Once);
            userRolesMockDbSet.Verify(m => m.Add(It.IsAny<UserRole>()), Times.Exactly(2));            
        }
    }
}
