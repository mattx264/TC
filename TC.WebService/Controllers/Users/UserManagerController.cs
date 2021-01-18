using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TC.DataAccess.Repositories;
using TC.WebService.Extensions;

namespace TC.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserManagerController : AuthBaseController
    {

        public UserManagerController(IUserHelper userHelper)
            :base(userHelper)
        {
           
        }
        [HttpGet]
        public IActionResult Get()
        {
            var user = GetUser();
            return Ok(new {
                Name= user.Name,
                Email=user.Email,
                Guid=user.Guid
            });
        }

    }
}