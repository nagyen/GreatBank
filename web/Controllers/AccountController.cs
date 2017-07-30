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
        // define services
        private UserTransactionService UserTransactionService { get; set; }

        public AccountController()
        {
            this.UserTransactionService = new UserTransactionService();
        }

        public IActionResult Index()
        {
            // if not logged-in redirect to login page
            if(!CheckAccess())
            {
                return Redirect("/");
            }
            var user = UserTransactionService.GetUser(GetCurrentUser());
            ViewData["FullName"] = $"{user.FirstName} {user.LastName}";
			return View();
        }

        // get user account details
        public IActionResult GetUserAccount()
        {
            if(!CheckAccess())
            {
                return NoAccess();
            }
            var user = UserTransactionService.GetUser(GetCurrentUser());

            return Json(new {
                username = user.Username,
                firstName = user.FirstName,
                lastName = user.LastName
            });
        }

		// get user account details
		public IActionResult GetTransactions(int page)
		{
			if (!CheckAccess())
			{
				return NoAccess();
			}
            var trans = UserTransactionService.GetTransactionsForUser(GetCurrentUser(), page)
                                              .Select(x => new {
										                date = x.Date.ToString("dd MMMM yy hh:mm:ss tt"),
										                type = x.GetTransactionType().ToString(),
										                amount = x.Amount,
										                prevBalance = x.PrevBalance,
										                currentBalance = x.CurrentBalance
										            });

            return Json(trans);
		}

        // get current balance for user
        public IActionResult GetCurrentBalance()
        {
			if (!CheckAccess())
			{
				return NoAccess();
			}
            return Json(UserTransactionService.GetCurrentBalanceForUser(GetCurrentUser()));
        }

		// deposit transactions
        [HttpPost]
        public IActionResult Deposit([FromBody]decimal amount)
		{
			if (!CheckAccess())
			{
				return NoAccess();
			}
            var res = UserTransactionService.Deposit(GetCurrentUser(), amount);
            if(res.Success)
            {
                return SuccessResponse();
            }
            return ErrorResponse(res.Errors);
		}

		// withdraw transaction
        [HttpPost]
        public IActionResult Withdraw([FromBody]decimal amount)
		{
			if (!CheckAccess())
			{
				return NoAccess();
			}
            var res = UserTransactionService.Withdraw(GetCurrentUser(), amount);
			if (res.Success)
			{
				return SuccessResponse();
			}
			return ErrorResponse(res.Errors);
		}
    }
}
