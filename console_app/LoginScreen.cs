using System;
using core;
using core.DbModels;

namespace console_app
{
    public class LoginScreen : BaseScreen
    {
        public override void Run(bool clearScreen = true)
        {
            if(clearScreen)
            {
				Console.Clear();
            }
            Console.WriteLine("1. Login User");
            Console.WriteLine("2. Go to Home Screen\n");
			if (Int32.TryParse(Console.ReadLine(), out int input))
			{
				switch (input)
				{
					case 1:
						{
							Login();
							break;
						}
					case 2:
						{
                            new HomeScreen().Run();
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

        // function that authenticates user
		public void Login()
		{
			Console.Write("Enter your username: ");
			var username = UserInputHelper.GetText();

			Console.Write("Enter your password: ");
			var pass = UserInputHelper.GetPassword();
			Console.WriteLine();

            var res = new AuthService().Login(username, pass);
			if (res.Success)
			{
				Console.WriteLine("Logged in : " + username);
                new AccountScreen(res.UserId, res.AuthKey).Run();
                return;
			}
			else
			{
				Console.WriteLine("Error : " + res.Errors);
                Login();
                return;
			}
		}

    }
}
