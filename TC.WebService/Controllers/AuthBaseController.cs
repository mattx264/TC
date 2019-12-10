using Microsoft.AspNetCore.Mvc;
using TC.DataAccess.Repositories;
using TC.Entity.Entities;
using TC.WebService.Helpers;

namespace TC.WebService.Controllers
{
    public class AuthBaseController : ControllerBase
    {
        protected IUserHelper _userHelper;

        public AuthBaseController(IUserHelper userHelper)
        {
            _userHelper = userHelper;
        }
        [NonAction]
        public string GetUserGuid()
        {
            return _userHelper.GetGuid(User);
        }
        [NonAction]
        public UserModel GetUser()
        {
            return _userHelper.GetUser(User); 
        }

    }
}
