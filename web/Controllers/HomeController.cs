using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using core;
namespace web.Controllers
{
    public class HomeController : Controller
    {
        public AuthService Authservice { get; set; }

        public HomeController()
        {
            Authservice = new AuthService();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login([FromBody]core.DbModels.User user)
		{
            var res = this.Authservice.Login(user.Username, user.Password);
            return Json(res);
		}

        public IActionResult Error()
        {
            return View();
        }
    }
}
