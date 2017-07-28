using System;
using core;
using core.DbModels;

namespace console_app
{
    public class AccountScreen: BaseScreen
    {
        private User User { get; set; }
        private Guid AuthKey { get; set; }
        private AuthService AuthService { get; set; }
        private UserTransactionService UserTransactionService { get; set; }

        public AccountScreen(long userId, Guid authKey)
        {
            UserTransactionService = new UserTransactionService();
            this.User = UserTransactionService.GetUser(userId);

			this.AuthKey = authKey;
			AuthService = new AuthService();
        }

		public override void Run(bool clearScreen = true)
		{
            if (User == null || !AuthService.IsAuthenticated(AuthKey))
			{
				Console.WriteLine("Could not validate user.");
				return;
			}

			Console.Clear();
            Console.WriteLine($"Welcome {User.Username}!\n");
			Console.WriteLine("1. Check Balance");
			Console.WriteLine("2. Deposit");
			Console.WriteLine("3. Withdrawl");
			Console.WriteLine("4. Transaction History");
            Console.WriteLine("5. Logout");


			if (Int32.TryParse(Console.ReadLine(), out int input))
			{
				switch (input)
				{
					case 1:
						{
							new RegisterScreen().Run();
							break;
						}
					case 2:
						{
							new LoginScreen().Run();
							break;
						}
					default:
						{
							ShowInvalidOptionError();
							break;
						}
				}
			}
			else
			{
				ShowInvalidOptionError();
			}
		}
    }
}
