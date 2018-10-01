using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace burgershack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : Controller //remember to inherent from Controller as opposed to ControllerBase
    {
        // GET api/values
        [HttpGet]  //this is a decorator so following method gets all properties of HttpGet class e.g., res.send().  these markups are stackable if need to. decorators are essentially for extension
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2", "value3", "value4" };
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
