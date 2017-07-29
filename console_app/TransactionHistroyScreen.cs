using System;
using core.DbModels;
using ConsoleTables;

namespace console_app
{
    public class TransactionHistoryScreen: BaseAccountScreen
    {
        public TransactionHistoryScreen(long userId, Guid authKey): base(userId, authKey)
		{
		}

        public override void Run(bool clearScreen = true)
        {
            ShowTransactions(1);
        }

        protected void ShowTransactions(int page = 1)
        {
			Console.Clear();
			Console.WriteLine($"Transactions: {User.Username}\n");
			var transactions = UserTransactionService.GetTransactionsForUser(User.Id, page);
            var table = new ConsoleTable("Date", "Type", "Amount", "Balance");
            foreach(var trans in transactions)
            {
                table.AddRow($"{trans.Date:MM/dd/yyyy hh:mm tt}", $"{trans.GetTransactionType().ToString()}", $"{trans.Amount}", $"{trans.CurrentBalance}");
            }
            table.Write();
            Console.WriteLine($" Current Page: {page}");
			Console.WriteLine("1. Next Page\t2. Prev Page\t3. Account Screen");

			if (Int32.TryParse(Console.ReadLine(), out int input))
			{
				switch (input)
				{
					case 1:
						{
                            ShowTransactions(++page);
							break;
						}
					case 2:
						{
                            if(page > 1)
                            {
                                ShowTransactions(--page);
                            }
                            else
                            {
                                ShowTransactions(1);
                            }
							break;
						}
					case 3:
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
