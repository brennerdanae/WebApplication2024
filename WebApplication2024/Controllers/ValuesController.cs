using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication2024.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<Guest> Get()
        {
            IList<Guest> guests = new List<Guest>();
            Guest guest1 = new Guest();
            guest1.Id = 20;
            guest1.Name = "Danae Brenner";
            guest1.Username = "danae.brenner";
            guest1.Email = "danae.brenner@gmail.com";
            guest1.Address = new Address();
            guest1.Address.Street = "1928 Bluebell Dr";
            guest1.Address.City = "Cincinnati";
            guest1.Address.Zipcode = "45224";
            guest1.Phone = "513-509-8132";
            guest1.Website = "fiveonefun.net";
            guests.Add(guest1);

            Guest guest2 = new Guest();
            guest2.Id = 22;
            guest2.Name = "Matthew Brenner";
            guest2.Username = "matt.brenner";
            guest2.Email = "matt.mbrenner@gmail.com";
            guest2.Address = new Address();
            guest2.Address.Street = "1928 Bluebell Dr";
            guest2.Address.City = "Cincinnati";
            guest2.Address.Zipcode = "45224";
            guest2.Phone = "513-338-3425";
            guest2.Website = "mattchoo.net";
            guests.Add(guest2);

            return guests;

        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public Guest Get(int id)
        {
            Guest guest3 = new Guest();
            guest3.Id = 21;
            guest3.Name = "Miles Brenner";
            guest3.Username = "miles.brenner";
            guest3.Email = "miles.dean@gmail.com";
            guest3.Address = new Address();
            guest3.Address.Street = "1928 Bluebell Dr";
            guest3.Address.City = "Cincinnati";
            guest3.Address.Zipcode = "45224";
            guest3.Phone = "513-123-4567";
            guest3.Website = "mrmilesdean.net";

            return guest3;
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
