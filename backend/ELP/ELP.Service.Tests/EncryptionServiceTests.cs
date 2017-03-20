using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ELP.Service.Tests
{
    public class EncryptionServiceTests
    {
        [Fact]
        public void CreateSaltTest()
        {
            IEncryptionService service = new EncryptionService();
            var result = service.CreateSalt();
            Assert.True(!String.IsNullOrWhiteSpace(result));
        }

        [Fact]
        public void EncryptPasswordTest()
        {
            IEncryptionService service = new EncryptionService();
            string salt = service.CreateSalt();
            var result = service.EncryptPassword("testPassword", salt);
            Assert.True(!String.IsNullOrWhiteSpace(result) && result != "testPassword");
        }
    }
}
