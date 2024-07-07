using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NHibernate.Cache;

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

        [Route("getAllGuest")]
        public IActionResult GetAllGuest()
        {
                try
                {
                var guests = _cacheProvider.GetCachedResponse().Result;
                    return Ok(guests);
                }
                catch (Exception ex)
                {
                    return new ContentResult()
                    {
                        StatusCode = 500,
                        Content = "{ \n error : " + ex.Message + "}",
                        ContentType = "application/json"
                    };
                }
          
        }


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
}
