using System;
using core.DbModels;

namespace core
{
    public class SeedTables
    {
        public static void Run()
        {
            // create demo user
            var user = new User
            {
                Username = "demouser",
                FirstName = "Demo",
                LastName = "User",
                Password = "demopass"
            };

            // add demo user
            new AuthService().Register(user);

            var transService = new UserTransactionService();

            var Random = new Random();
            // create transactions
            for (var i = 0; i < 1000; i++)
            {
                //set transaciton type
                Transaction.TransactionType type;
                if(Random.NextDouble() > 0.5)
                {
                    type = Transaction.TransactionType.Withdrawal;
                }
                else
                {
                    type = Transaction.TransactionType.Deposit;
                }
                // set amount for transaction
                var amount = (decimal)Random.NextDouble() * 100;
                //save
                transService.AddTransaction(user.Id, amount, type);
            }
        }
    }
}
