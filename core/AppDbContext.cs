using System;
using Microsoft.EntityFrameworkCore;
using core.DbModels;

namespace core
{
    public class AppDbContext : DbContext
    {
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
            optionsBuilder.UseInMemoryDatabase("GreatBank");
		}

		public DbSet<User> Users { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

		public DbSet<Auth> Auths { get; set; }

	}
}
