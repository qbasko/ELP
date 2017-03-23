using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELP.WebApi.Models
{
    public class UserDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
