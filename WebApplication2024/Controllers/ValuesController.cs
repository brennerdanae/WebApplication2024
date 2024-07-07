using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NHibernate.Cache;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication2024.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private ICacheProvider _cacheProvider;
        public ValuesController(ICacheProvider cacheProvider)
        {
            _cacheProvider = cacheProvider;
        }

        //[Route("getAllGuest")]
        //public IActionResult GetAllGuest()
        //{
        //    try
        //    {
        //        var guests = _cacheProvider.GetCachedResponse().Result;
        //        return Ok(guests);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ContentResult()
        //        {
        //            StatusCode = 500,
        //            Content = "{ \n error : " + ex.Message + "}",
        //            ContentType = "application/json"
        //        };
        //    }

        //}

        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<Guest> Get()
        {
            return GuestRoster.allGuests;
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public Guest Get(int id)
        {
            return GuestRoster.allGuests[id];
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] Guest value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Guest value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

    public class LoginController: Controller
    {
        private IConfiguration _config;
        
        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserModel login)
        {
            IActionResult response = Unauthorized();
            var user = AuthenticateUser(login);
            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            }
            return response;
        }

        private string GenerateJSONWebToken(UserModel userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                null,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserModel AuthenticateUser(UserModel login)
        {
            UserModel user = null;

            //validate the User Credentials demo
            if(login.Username == "Danae")
            {
                user = new UserModel { Username = "Danae Brenner", EmailAddress = "danae.brenner@gmail.com" };
            }
            return user;

        }

    }
}
