using ELP.Model.Entities;
using System;
using System.Collections.Generic;
using Xunit;


namespace ELP.Service.Tests
{
    public class MembershipServiceTests
    {
        [Fact]
        public void CreateUserTest()
        {
            IMembershipService service = new MembershipService();
            User result = service.CreateUser("testUser1","test@email.com","password",new List<int>(){ 1,2});
            Assert.NotNull(result);
        }
    }
}
