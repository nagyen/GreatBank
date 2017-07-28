using System;
using core;
using core.DbModels;

namespace console_app
{
    public class RegisterScreen : BaseScreen
    {
        public override void Run(bool clearScreen = true)
        {
            Console.Clear();
            Console.WriteLine("1. Register New User");
			Console.WriteLine("2. Go to Home Screen\n");
			if (Int32.TryParse(Console.ReadLine(), out int input))
			{
				switch (input)
				{
					case 1:
						{
							Register();
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

        // function that registers new user
        public void Register()
        {
			Console.Write("Enter your username: ");
			var username = UserInputHelper.GetText();

			Console.Write("Enter your password: ");
			var pass = UserInputHelper.GetPassword();
            Console.WriteLine();
			
            Console.Write("Re-enter your password: ");
			var confirmpass = UserInputHelper.GetPassword();
            Console.WriteLine();

			if (pass != confirmpass)
			{
				Console.WriteLine("Passwords not matched.");
                Register();
                return;
			}

			var user = new User
			{
				Username = username,
				Password = pass
			};

			var res = new AuthService().Register(user);
			if (res.Success)
			{
				Console.WriteLine("Registered : " + username);

                new LoginScreen().Run(false);
                return;
			}
			else
			{
				Console.WriteLine("Error : " + res.Errors);
                Register();
                return;
			}
        }

    }
}
