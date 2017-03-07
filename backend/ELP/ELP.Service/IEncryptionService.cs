using System;
using System.Collections.Generic;
using System.Text;

namespace ELP.Service
{
    public interface IEncryptionService
    {
        string CreateSalt();
        string EncryptPassword(string password, string salt);
    }
}
