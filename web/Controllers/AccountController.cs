using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using core;

namespace web.Controllers
{
    public class AccountController : BaseController
    {
        public IActionResult Index()
        {
            // if not logged in redirect to login page
            if(!CheckAccess())
            {
                return Redirect("/");
            }
            return View();
        }
    }
}
