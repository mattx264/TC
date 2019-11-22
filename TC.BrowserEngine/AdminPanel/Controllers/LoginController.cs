using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TC.BrowserEngine.AdminPanel.DataAccess;
using TC.BrowserEngine.AdminPanel.DataAccess.Models;
using TC.BrowserEngine.AdminPanel.ViewModels;
using TC.BrowserEngine.Helpers;
using TC.BrowserEngine.Helpers.ApiCall;
using TC.Entity.Entities;

namespace TC.BrowserEngine.AdminPanel.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private LocalUserRepository _localUserRepository;

        public LoginController()
        {
            _localUserRepository =new LocalUserRepository();
        }
        [HttpGet]
        public ActionResult Get()
        {

            return Ok("This is my default action...");
        }
        [HttpPost]
        public async Task<ActionResult> PostAsync(LoginViewModel loginViewModel)
        {
            var token= await Login.LoginAsync(loginViewModel.Email, loginViewModel.Password);
            if(token == null)
            {
                return Unauthorized();
            }
            var userModel =await new ApiCall<UserModelViewModel>().GetAsync("api/UserManager", token);
            _localUserRepository.SetOrUpdateLocalUser(new LocalUser()
            {
                CreateDate = DateTime.Now,
                Guid= userModel.Guid,
                IsActive=true,
                ModifiedDate=DateTime.Now,
                Name= userModel.Name,
                Token=token
            });
            BrowserEngineManager.Instance.StartInstances();
            return Ok();
        }

        private object Date()
        {
            throw new NotImplementedException();
        }
    }
}
