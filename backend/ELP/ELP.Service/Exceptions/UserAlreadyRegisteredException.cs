using System;
using System.Collections.Generic;
using System.Text;

namespace ELP.Service.Exceptions
{
    public class UserAlreadyRegisteredException : Exception
    {
        public UserAlreadyRegisteredException() : base("User already exists!")
        {

        }
    }
}
