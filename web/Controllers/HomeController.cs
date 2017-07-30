using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace web.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            // go to account screen if logged in already
            if(CheckAccess())
            {
                return Redirect("/account");
            }
            // else show login page
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
