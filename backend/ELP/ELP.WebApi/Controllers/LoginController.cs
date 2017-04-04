using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ELP.Model.Entities;
using ELP.Service;
using ELP.WebApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ELP.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // GET api/ping
        [HttpGet("ping")]
        public string Ping()
        {
            return DateTime.UtcNow.ToString();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto user)
        {

            GenericResult result = null;
            try
            {
                IdentityResult createdUser = await _userService.CreateUser(new IdentityUser(user.Username), user.Password);

                if (user != null)
                {
                    result = new GenericResult()
                    {
                        Success = true,
                        Message = "Registration succeeded"
                    };
                }
                else
                {
                    result = new GenericResult()
                    {
                        Success = false,
                        Message = "Registration failed"
                    };
                }
            }
            catch (Exception ex)
            {
                result = new GenericResult()
                {
                    Success = false,
                    Message = ex.Message
                };
            }

            return new ObjectResult(result);
        }

        [HttpPost("signin")]
        public IActionResult SignIn([FromBody] UserDto user)
        {
            return new ObjectResult(new GenericResult()
            {
                Success = true,
                Message = DateTime.UtcNow.ToString()
            });
        }
    }
}
