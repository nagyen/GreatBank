using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using core;
using Microsoft.AspNetCore.Http;

namespace web.Controllers
{
	public abstract class BaseController : Controller
	{
        // cookies
        private string _authCookie = "GB-Auth";
		private string _userCookie = "GB-User";

        // services
		public AuthService Authservice { get; set; }

        // constructor
		public BaseController()
		{
            // set services
			Authservice = new AuthService();
		}

		// write cookies
        protected void WriteCookies(string setting, string settingValue)
		{
            // create server only cookie
            CookieOptions options = new CookieOptions()
            {
                Path = "/",
                HttpOnly = true,
                Secure = false,
                Expires = DateTime.Now.AddHours(1)
            };
            // write
			Response.Cookies.Append(setting, settingValue, options);
		}

        // read cookies
        protected string ReadCookies(string cookieName)
		{
			return Request.Cookies[cookieName];
		}

        // get authentication key
        public string GetAuthKey()
        {
            return ReadCookies(_authCookie);
        }

		// get user id
		public long GetCurrentUser()
		{
            return long.Parse(ReadCookies(_userCookie));
		}

		// set authentication key
        public void SetAuth(long userId, Guid key)
		{
            WriteCookies(_userCookie, userId.ToString());
            WriteCookies(_authCookie, key.ToString());
		}

		// check if user is authenticated
        public bool CheckAccess()
        {
            // check if auth key is valid
            if(Guid.TryParse(GetAuthKey(), out Guid authkey))
            {
                return Authservice.IsAuthenticated(authkey);
            }
            // if here no auth key
            return false;
        }

        // error respose
        public IActionResult ErrorResponse(string error)
        {
            return Json(new
            {
                code = 1,
                text = error
            });
        }
		// error respose
		public IActionResult SuccessResponse(string redirect = "")
		{
			return Json(new
			{
				code = 0,
                redirect = redirect
			});
		}

        // return no access error
        public IActionResult NoAccess()
        {
            return ErrorResponse("No Access");
        }
	}
}
