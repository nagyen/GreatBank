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
                return db.Transactions.Where(x => x.UserId == userId).ToList();
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
                return db.Transactions.Where(x => x.UserId == userId).Skip(startFrom).Take(limit).OrderByDescending(x => x.Date).ToList();
			}
		}

        // deposit
        public void Deposit(long userId, decimal amount)
        {
            AddTransaction(userId, amount, Transaction.TransactionType.Deposit);
        }

		// withdrawl
		public void Withdraw(long userId, decimal amount)
		{
            AddTransaction(userId, amount, Transaction.TransactionType.Withdrawl);
		}

        // funciton that creates a transaction for user
        private void AddTransaction(long userId, decimal amount, Transaction.TransactionType transactionType)
        {
			// amount should be positive
			if (amount < 0) return;

			// check if user valid
			var user = GetUser(userId);
			if (user == null) return;

			// create withdraw transaction for user 
			var prevTrans = GetLastTransactionForUser(userId);
            var prevBalance = prevTrans?.CurrentBalance ?? 0;
			var transaction = new Transaction
			{
				UserId = userId,
				Date = DateTime.Now,
                Type = (int)transactionType,
				Amount = amount,
                PrevBalance = prevBalance,
                CurrentBalance = prevBalance + (transactionType == Transaction.TransactionType.Deposit ? amount : -amount)
			};

			//add
			using (var db = new AppDbContext())
			{
				db.Transactions.Add(transaction);
				db.SaveChanges();
			}
        }
    }
}
