using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace TC.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ValuesController : ControllerBase
    {
        private IDistributedCache _distributedCache;
        private readonly ILogger<ValuesController> _logger;

        public ValuesController(IDistributedCache distributedCache, ILogger<ValuesController> logger)
        {
            _logger = logger;
            _distributedCache = distributedCache;

        }
        // GET api/values
        [HttpGet]

        public ActionResult<IEnumerable<string>> Get()
        {
       
            var test = _distributedCache.GetAsync("test");
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [Authorize]
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
