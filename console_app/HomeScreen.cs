using System;
namespace console_app
{
    public class HomeScreen: BaseScreen
    {
        public override void Run()
        {
			Console.WriteLine("1. Register");
			Console.WriteLine("2. Login");

			if (Int32.TryParse(Console.ReadLine(), out int input))
			{
                switch(input)
                {
                    case 1:
                        {
                            new RegisterScreen().Run();
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
