using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using core.DbModels;

namespace core
{
    public class UserTransactionService
    {
        // get all users
        public IEnumerable<User> GetUsers()
        {
			using (var db = new AppDbContext())
			{
                return db.Users.ToList();
			}
        }

        // get user transactions
        public IEnumerable<Transaction> GetTransactionsForUser(long userId)
        {
            using(var db = new AppDbContext())
            {
                return db.Transactions.Where(x => x.UserId == userId).ToList();
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
    }
}
