using ELP.Model;
using ELP.Model.Entities;
using ELP.Service.Tests.Common;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;

namespace ELP.Service.Tests
{
    public class RoleServiceTests
    {
        [Fact]
        public void GetRoleByIdTest()
        {
            var ctx = new Mock<IContext>();
            List<Role> roles = new List<Role>();
            roles.Add(new Role() { Id = 1, Name = "Role1" });
            roles.Add(new Role() { Id = 2, Name = "Role2" });
            var rolesMockDbSet = ServiceTestsHelper.GetMockDbSet<Role>(roles);

            ctx.Setup(c => c.Set<Role>()).Returns(rolesMockDbSet.Object);
            IRoleService roleService = new RoleService(ctx.Object);
            var role = roleService.GetRoleById(1);
            Assert.Equal("Role1", role.Name);
        }
    }
}
