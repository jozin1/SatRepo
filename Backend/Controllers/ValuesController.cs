using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using EFCdemo;
using System.Linq;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SatellitesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<string>))]
        public ActionResult<IEnumerable<string>> Get()
        {
            using (var context = new SatContext())
            {
                string response = null;
                DataContractJsonSerializer Serializer = new DataContractJsonSerializer(typeof(List<Sat>));
                MemoryStream stream = new MemoryStream();
                Serializer.WriteObject(stream, TypesContainer.spagetti);  //tu trzeba wstawić coś typu List<Sat> a nie DbSet<Sat>!
                stream.Position = 0;
                StreamReader sr = new StreamReader(stream);
                response = sr.ReadToEnd();

                return Ok(response);
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
