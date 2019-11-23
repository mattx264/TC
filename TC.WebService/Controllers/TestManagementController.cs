using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TC.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestManagementController : ControllerBase
    {
        public IActionResult Get()
        {
            return Ok();
        }
        public IActionResult Post()
        {
            return Created("http://example.org/myitem", new { name = "testitem" });

        }
        public IActionResult Put()
        {
            return Ok();
        }
    }
}