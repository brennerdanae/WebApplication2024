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
        /*
         *private readonly DbContext _context
         *public ValuesController(DbContext context)
         *{
         *  _context = context;
         *}
         */

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

        /*
         *[HttpGet]
         *[Authorize]
         *public IActionResult Get()
         *{
         *  try
         *  {
         *      var guests = _context.Guests.ToList();
         *      if(guests.Count == 0)
         *      {
         *          return NotFound("No data Found.");
         *      }
         *      return Ok(guests);
         *  }
         *  catch (Exception ex)
         *  {
         *  return BadRequest(ex.Message);
         *  }
         */


        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public Guest Get(int id)
        {
            return GuestRoster.allGuests[id];
        }

        /*
         * [HttpGet("{id}")]
         * public IActionResult Get(int id)
         * {
         *      try
         *      {
         *          var guest = _context.Guests.Find(id);
         *          if (guest == null)
         *          {
         *              return NotFound($"Guest not found with id {id}");
         *          }
         *          return Ok(guest);
         *      }
         *      catch  (Exception ex)
         *      {
         *          BadRequest(ex.Message);
         *      }
         * }
         */

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] Guest value)
        {
        }

        /*
         *[HttpPost]
         * public IActionResult Post(Guest value)
         * {
         *      try
         *      {
         *      
         *          _context.Add(value);
         *          _context.SaveChanges();
         *          
         *          return Ok("Guest Created);
         *      }
         *      catch (Exception ex)
         *      {
         *          return BadRequest(ex.Message);
         *      }
         * }
         */

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Guest value)
        {
        }

        /*
         *[HttpPut("{id}")]
         *public IActionResuolt Post(Guest value)
         *{
         *  if (value==null || value.id==0)
         *  {
         *      if (value==null)
         *      {
         *          return BadRequest("Guest data is invalid");
         *      }
         *      if (value.id ==0)
         *      {
         *          return BadRequest($"Guest id {value.id} is invalid");
         *      }
         *  }
         *  try
         *  {
         *      var value = _context.Guests.Find(value.id);
         *      if(value ==null)
         *      {
         *          return BadRequest("");
         *      }
         *      value.
         *      value.
         *      value.
         *      value.
         *      value.
         *      _context.SaveChanges();
         *      
         *      return Ok("Guest details updated.");
         *  }
         *  catch (Exception ex)
         *  {
         *      return BadRequest(ex.Message);
         *  }
         *}
         */

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        /*
         *[HttpDelete("{id}")]
         *public IActionResult Delete(int id)
         *{
         *  try
         *  {
         *      var guest = _context.Guests.Find(id);
         *      if (guest == null)
         *      {
         *          return NotFound($"Guest not found with id {id}");
         *      }
         *      _context.Guests.remove(guest);
         *      _context.SaveChanges();
         *      return Ok("Guest deleted.");
         *  }
         *  catch (Exception ex)
         *  {
         *      return BadRequest(ex.Message);
         *  }
         *}
         */
    }

}
