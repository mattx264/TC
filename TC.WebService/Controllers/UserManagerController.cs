using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TC.DataAccess.Repositories;
using TC.WebService.Helpers;

namespace TC.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserManagerController : ControllerBase
    {
        private UserRepository _userRepository;

        public UserManagerController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public IActionResult Get()
        {
            string guid = UserHelper.GetGuid(User);
            var user = _userRepository.GetByGuid(guid);
            return Ok(new {
                Name= user.Name,
                Email=user.Email,
                Guid=user.Guid
            });
        }

    }
}