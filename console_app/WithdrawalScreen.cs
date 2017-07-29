using System;
namespace console_app
{
    public class WithdrawalScreen : BaseAccountScreen
    {
        public WithdrawalScreen(long userId, Guid authKey) : base(userId, authKey)
        {
        }

        public override void Run(bool clearScreen = true)
        {
            if (clearScreen) Console.Clear();
            Console.WriteLine($"Withdrawal for {User.Username}");
            Console.WriteLine($"Availabe for withdrawal: {UserTransactionService.GetCurrentBalanceForUser(User.Id):C}\n");

			Console.Write("Enter amount to withdraw: ");
            var amount = UserInputHelper.GetDecimal();
            Console.Write($"Withdraw {amount:C}. Are you sure? (y/n) : ");
            var confirm = Console.ReadLine();
            if(confirm == "y" || confirm == "Y")
            {
                var res = UserTransactionService.Withdraw(User.Id, amount);
				if (res.Success)
				{
					Console.WriteLine("Withdrawal Success!");
				}
				else
				{
					Console.WriteLine($"Withdrawal Failed. {res.Errors}");
				}

            }
            else
            {
                Console.WriteLine("Withdrawal Cancelled!");
            }
            ShowDoneOptions();
        }

        protected void ShowDoneOptions()
        {
			Console.WriteLine("\n1. Make Another Withdrawal: ");
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
