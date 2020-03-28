using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TC.BrowserEngine.AdminPanel.DataAccess;
using TC.BrowserEngine.Controllers;
using TC.BrowserEngine.Helpers.Enums;
using TC.Common.Selenium;
using TC.Common.Selenium.WebDriverOperation;

namespace TC.BrowserEngine.AdminPanel.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestRunController : ControllerBase
    {
        private LocalUserRepository _localUserRepository;

        public TestRunController()
        {
            _localUserRepository = new LocalUserRepository();
        }
        [HttpPost]
        public ActionResult PostAsync(List<SeleniumCommand> commands)
        {
            return Ok();
        }
    }
}
