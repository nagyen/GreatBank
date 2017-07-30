using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using core.DbModels;

namespace core
{
    public class UserTransactionService
    {
        // get single user
        public User GetUser(long id)
        {
			using (var db = new AppDbContext())
			{
                return db.Users.FirstOrDefault(x => x.Id == id);
			}
        }

        // get user with transactions
        public User GetUserWithTransactions(long userId)
        {
            using (var db = new AppDbContext())
            {
                return db.Users.Include(x => x.Transactions).FirstOrDefault(x => x.Id == userId);
            }
        }

        // get current balance for user
        public decimal GetCurrentBalanceForUser(long userId)
        {
            return GetLastTransactionForUser(userId)?.CurrentBalance ?? 0;
        }

        // get most recent transaction for user
        public Transaction GetLastTransactionForUser(long userId)
        {
			using (var db = new AppDbContext())
			{
                return db.Transactions.Where(x => x.UserId == userId).OrderByDescending(x => x.Date).FirstOrDefault();
			}
        }

        // get all user transactions
        public IEnumerable<Transaction> GetTransactionsForUser(long userId)
        {
            using(var db = new AppDbContext())
            {
                return db.Transactions.Where(x => x.UserId == userId).OrderByDescending(x => x.Date).ToList();
            }
        }

		// get limited user transactions
		public IEnumerable<Transaction> GetTransactionsForUser(long userId, int page)
		{
            if (page < 1) page = 1;
			var limit = 10;
            var startFrom = (page - 1)  * limit;
			using (var db = new AppDbContext())
			{
                return db.Transactions.Where(x => x.UserId == userId).OrderByDescending(x => x.Date).Skip(startFrom).Take(limit).ToList();
			}
		}

        // deposit
        public TransactionModels.TransactionFeedback Deposit(long userId, decimal amount)
        {
            return AddTransaction(userId, amount, Transaction.TransactionType.Deposit);
        }

		// withdrawl
		public TransactionModels.TransactionFeedback Withdraw(long userId, decimal amount)
		{
            return AddTransaction(userId, amount, Transaction.TransactionType.Withdrawal);
		}

        // funciton that creates a transaction for user
        public TransactionModels.TransactionFeedback AddTransaction(long userId, decimal amount, Transaction.TransactionType transactionType)
        {
			// amount should be positive
			if (amount < 0)
            {
				// return error
				return new TransactionModels.TransactionFeedback
				{
					Success = false,
					Errors = "Amount should be more than 0",
					TransactionType = transactionType
				};
            }

			// check if user valid
			var user = GetUser(userId);
			if (user == null)
            {
				// return error
				return new TransactionModels.TransactionFeedback
				{
					Success = false,
					Errors = "Cannot validate user.",
					TransactionType = transactionType
				};
            }

			// create withdraw transaction for user 
			var prevTrans = GetLastTransactionForUser(userId);
            var prevBalance = prevTrans?.CurrentBalance ?? 0;
            // check if new balance is below limit
            var currBal = prevBalance + (transactionType == Transaction.TransactionType.Deposit ? amount : -amount);
            if(currBal < 0)
            {
                // return error
                return new TransactionModels.TransactionFeedback
                {
                    Success = false,
                    Errors = "Cannot withdraw more than available balance.",
                    TransactionType = transactionType
                };
            }
			var transaction = new Transaction
			{
				UserId = userId,
				Date = DateTime.Now,
                Type = (int)transactionType,
				Amount = amount,
                PrevBalance = prevBalance,
                CurrentBalance = currBal
			};

			//add
			using (var db = new AppDbContext())
			{
				db.Transactions.Add(transaction);
				db.SaveChanges();
			}

            // return 
			return new TransactionModels.TransactionFeedback
			{
                Success = true,
				TransactionType = transactionType
			};
        }
    }
}
