using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TC.BrowserEngine.AdminPanel.DataAccess;

namespace TC.BrowserEngine.AdminPanel.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogoutController : ControllerBase
    {
        private LocalUserRepository _localUserRepository;

        public LogoutController()
        {
            _localUserRepository = new LocalUserRepository();
        }
        [HttpPost]
        public ActionResult Post()
        {
            _localUserRepository.LogoutCurrentUser();
            BrowserEngineManager.Instance.StopInstances();
            return Ok();
        }
    }
}
