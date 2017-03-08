using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using ELP.Model;
using ELP.Model.Entities;

namespace ELP.Service.Tests
{
    public class UserServiceTests
    {
        [Fact]
        public void GetUserByUsernameTest()
        {
            var ctx = new Mock<IContext>();
            ctx.SetupSet<User>(u => u.Users.Add(new User() { Username = "testUser1" }));
            IUserService service = new UserService(ctx.Object);
            var result = service.GetUserByUsername("testUser1");
            Assert.NotNull(result);
        }
    }
}
