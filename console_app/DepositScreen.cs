using System;
namespace console_app
{
    public class DepositScreen : BaseAccountScreen
    {
        public DepositScreen(long userId, Guid authKey) : base(userId, authKey)
        {
        }

        public override void Run(bool clearScreen = true)
        {
            if (clearScreen) Console.Clear();
            Console.WriteLine($"Deposit for {User.Username}\n");
            Console.Write("Enter amount to deposit: ");
            var amount = UserInputHelper.GetDecimal();
            Console.Write($"Depost {amount:C}. Are you sure? (y/n) : ");
            var confirm = Console.ReadLine();
            if(confirm == "y" || confirm == "Y")
            {
                var res = UserTransactionService.Deposit(User.Id, amount);
                if(res.Success)
                {
					Console.WriteLine("Deposit Success!");
                }
                else
                {
                    Console.WriteLine($"Deposit Failed. {res.Errors}");
                }
            }
            else
            {
                Console.WriteLine("Deposit Cancelled!");
            }
            ShowDoneOptions();
        }

        protected void ShowDoneOptions()
        {
			Console.WriteLine("\n1. Make Another Deposit: ");
			Console.WriteLine("2. Back to Account Screen");
            Console.Write("\nPlease enter your choice: ");
			if (Int32.TryParse(Console.ReadLine(), out int input))
			{
				switch (input)
				{
					case 1:
						{
                            Run(false);
							break;
						}
					case 2:
						{
							new AccountScreen(User.Id, AuthKey).Run();
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
