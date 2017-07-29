using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using core;
namespace web.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login([FromBody]AuthModels.LoginRegister login)
		{
            // login 
            var res = this.Authservice.Login(login.Username, login.Password);
            if(res.Success)
            {
                // set auth key for session
                SetAuth(res.UserId, res.AuthKey);

                return SuccessResponse("/account");
            }
            // return error
            return ErrorResponse(res.Errors);
		}

        public IActionResult Error()
        {
            return View();
        }
    }
}
