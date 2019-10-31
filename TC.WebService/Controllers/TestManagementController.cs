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
        public async Task<IActionResult> Get()
        {
            return Ok();
        }
        public async Task<IActionResult> Post()
        {
            return Created("http://example.org/myitem", new { name = "testitem" });
            
        }
        public async Task<IActionResult> Put()
        {
            return Ok();
        }
    }
}