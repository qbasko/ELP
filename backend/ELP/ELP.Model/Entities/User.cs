using ELP.Model.Common;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELP.Model.Entities
{
    public class User : IdentityUser, IEntity<string>
    {
        public User()
        {

        }
        public User(string userName) : base(userName)
        {

        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
