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
using System.ComponentModel.DataAnnotations;
using ELP.WebApi.Filters;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ELP.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<LoginController> _logger;
        private readonly IConfigurationRoot _config; //TODO it doesnt work, can't resolve it

        public LoginController(IUserService userService, ILogger<LoginController> logger, IConfigurationRoot config)
        {
            _userService = userService;
            _logger = logger;
            _config = config;
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

        // GET api/ping
        [Authorize]
        [HttpGet("pingAuth")]
        public string PingWithAutorize()
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
                User newUser = new User(user.Username);
                newUser.Email = user.Email;
                newUser.FirstName = user.FirstName;
                IdentityResult createdUser = await _userService.CreateUser(newUser, user.Password);

                if (createdUser != null && createdUser.Succeeded)
                {
                    result = new GenericResult()
                    {
                        Success = createdUser.Succeeded,
                        Message = "Registration succeeded"
                    };
                }
                else
                {
                    result = new GenericResult()
                    {
                        Success = false,
                        Message = createdUser.Errors.First().Description
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                result = new GenericResult()
                {
                    Success = false,
                    Message = "Registration failed"
                };
            }

            return new ObjectResult(result);
        }

        [ValidateModel]
        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] UserDto user)
        {
            var result = await _userService.SignIn(user.Username, user.Password);

            if (result.Succeeded)
                return Ok();

            //return new ObjectResult(new GenericResult()
            //{
            //    Success = result.Succeeded,
            //    Message = DateTime.UtcNow.ToString()
            //});

            return BadRequest("Failed to login");
        }

        [ValidateModel]
        [HttpPost("token")]
        public async Task<IActionResult> CreateToken([FromBody] UserDto userDto)
        {
            try
            {
                var user = await _userService.GetUserByUsername(userDto.Username);
                if (user != null)
                {
                    if (_userService.VerifyHashedPassword(user, userDto.Password) == PasswordVerificationResult.Success)
                    {
                        var userClaims = await _userService.GetClaims(user);

                        var claims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                            new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                            new Claim(JwtRegisteredClaimNames.Email, user.Email)
                        }.Union(userClaims);

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(
                            issuer: _config["Tokens:Issuer"],
                            audience: _config["Tokens:Audience"],
                            claims: claims,
                            expires: DateTime.UtcNow.AddMinutes(15),
                            signingCredentials: creds
                            );

                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return BadRequest("Failed to login");
        }


        [HttpGet("externalLogin")]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLogin([FromBody] ExternalLoginDto extLoginDto)
        {
            // Request a redirect to the external login provider.
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Login", new { ReturnUrl = extLoginDto.RedirectUrl });
            var properties = _userService.ConfigureExternalAuthenticationProperties(extLoginDto.Provider, redirectUrl);
            return Challenge(properties, extLoginDto.Provider);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            return Ok();
        }
    }
}
