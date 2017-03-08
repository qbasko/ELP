using ELP.Model.Entities;
using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using ELP.Model;

namespace ELP.Service.Tests
{
    public class MembershipServiceTests
    {
        [Fact]
        public void CreateUserTest()
        {
            var ctx = new Mock<IContext>();
            ctx.SetupSet<User>(u => u.Users.Add(new User() { Username = "testUser1" }));
            var userService = new UserService(ctx.Object);
            var encryptionService = new EncryptionService();
            IMembershipService service = new MembershipService(userService, encryptionService);
            User result = service.CreateUser("testUser1","test@email.com","password",new List<int>(){ 1,2});
            Assert.NotNull(result);
        }
    }
}
