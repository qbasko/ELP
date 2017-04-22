using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELP.WebApi.Models
{
    public class ExternalLoginDto
    {
        public string Provider { get; set; }
        public string RedirectUrl { get; set; }
    }
}
