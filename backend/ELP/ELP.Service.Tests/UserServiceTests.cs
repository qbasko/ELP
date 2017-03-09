using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using ELP.Model;
using ELP.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ELP.Service.Tests.Common;

namespace ELP.Service.Tests
{
    public class UserServiceTests
    {
        [Fact]
        public void GetUserByUsernameTest()
        {
            string username = "testUser1";
            var ctx = new Mock<IContext>();
            List<User> users = new List<User>();
            users.Add(new User() { Username = username});
            var mockDbSet = ServiceTestsHelper.GetMockDbSet<User>(users);            
            ctx.Setup(c => c.Set<User>()).Returns(mockDbSet.Object);
            IUserService service = new UserService(ctx.Object);
            var result = service.GetUserByUsername(username);
            Assert.NotNull(result);
        }    

    }
}
