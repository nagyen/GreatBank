using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using core;
namespace web.Controllers
{
    public class AuthController : BaseController
    {
		[HttpPost]
		public IActionResult Login([FromBody]AuthModels.Login login)
		{
			// login 
			var res = this.Authservice.Login(login.Username, login.Password);
			if (res.Success)
			{
				// set auth key for session
				SetAuth(res.UserId, res.AuthKey);

				return SuccessResponse("/account");
			}
			// return error
			return ErrorResponse(res.Errors);
		}

		[HttpPost]
        public IActionResult Register([FromBody]AuthModels.Register register)
		{
            // check passwords
            if(register.Password != register.ConfirmPassword)
            {
                return ErrorResponse("Passwords not matched.");
            }

            // define user
            var newuser = new core.DbModels.User
            {
                Username = register.Username,
                Password = register.Password,
                FirstName = register.FirstName,
                LastName = register.LastName
            };

			// register user 
            var res = this.Authservice.Register(newuser);
			if (res.Success)
			{
				return SuccessResponse("/");
			}
			// return error
			return ErrorResponse(res.Errors);
		}

	}
}
