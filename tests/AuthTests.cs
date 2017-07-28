using System;
using core;

namespace tests
{
    public class AuthTests
    {
        private AuthService AuthService { get; set; }
        public AuthTests()
        {
            AuthService = new AuthService();
        }

        // run all tests
        public static void Run()
        {
            var test = new AuthTests();

            // register
            test.RegisterWithValidUser();
            test.RegisterWithDuplicateUser();
            test.RegisterWithEmptyUser();
            test.RegisterWithEmptyPassword();

            // login
            test.SuccessLogin();
            test.WrongUsernameLogin();
            test.WrongPasswordLogin();

            //logout
            test.Logout();
        }

        // register with valid user
        public void RegisterWithValidUser()
        {
            Console.WriteLine("Register: Should log new user id.");
            var newUser = new core.DbModels.User
            {
                Username = "testUser",
                Password = "testPass"
            };
            var res = AuthService.Register(newUser);
            Console.WriteLine($" Success: {res.Success} \n Errors: {res.Errors} \n New user id: {newUser.Id}");
            Console.WriteLine();
        }

		// register with in-valid user
		public void RegisterWithDuplicateUser()
		{
			Console.WriteLine("Register: Should show user exits");
			var newUser = new core.DbModels.User
			{
				Username = "testUser",
				Password = "testPass"
			};
			var res = AuthService.Register(newUser);
			Console.WriteLine($" Success: {res.Success} \n Errors: {res.Errors} \n New user id: {newUser.Id}");
			Console.WriteLine();
		}

		// register with empty user name
		public void RegisterWithEmptyUser()
		{
			Console.WriteLine("Register: Empty user. Should show error");
			var newUser = new core.DbModels.User
			{
				Username = "",
				Password = "testPass"
			};
			var res = AuthService.Register(newUser);
			Console.WriteLine($" Success: {res.Success} \n Errors: {res.Errors} \n New user id: {newUser.Id}");
			Console.WriteLine();
		}

		// register with empty password
		public void RegisterWithEmptyPassword()
		{
			Console.WriteLine("Register: Empty password. Should show error");
			var newUser = new core.DbModels.User
			{
				Username = "user2",
				Password = ""
			};
			var res = AuthService.Register(newUser);
			Console.WriteLine($" Success: {res.Success} \n Errors: {res.Errors} \n New user id: {newUser.Id}");
			Console.WriteLine();
		}

        // success login
        public void SuccessLogin()
        {
			Console.WriteLine("Login: should show Auth key.");
			
            var res = AuthService.Login("testUser", "testPass");
            Console.WriteLine($" Success: {res.Success} \n Errors: {res.Errors} \n Auth: {res.AuthKey}");
            Console.WriteLine();
        }

        // wrong password login
        public void WrongPasswordLogin()
        {
			Console.WriteLine("Login: Incorrect password. should show error.");

			var res = AuthService.Login("testUser", "testPass2");
			Console.WriteLine($" Success: {res.Success} \n Errors: {res.Errors} \n Auth: {res.AuthKey}");
			Console.WriteLine();
        }

        // wrong username login
		public void WrongUsernameLogin()
		{
			Console.WriteLine("Login: Incorrect username. should show error.");

			var res = AuthService.Login("testUser2", "testPass");
			Console.WriteLine($" Success: {res.Success} \n Errors: {res.Errors} \n Auth: {res.AuthKey}");
			Console.WriteLine();
		}

        // log out
        public void Logout()
        {
			Console.WriteLine("Logout:");
            AuthService.Logout("testUser");
			Console.WriteLine($" Logged out.");
			Console.WriteLine();
        }
    }
}
