using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELP.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class EventsController : Controller
    {
        // GET: api/values
        [Authorize(Policy = "SuperUser")]
        [HttpGet("")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
    }
}
