﻿using System;
using core;
using core.DbModels;

namespace core
{
    class Program
    {
        static void Main(string[] args)
        {
			using (var db = new AppDbContext())
			{
				db.Users.Add(new User { Username = "test" });
				var count = db.SaveChanges();
				Console.WriteLine("{0} records saved to database", count);

				Console.WriteLine();
				Console.WriteLine("All Users in database:");
				foreach (var user in db.Users)
				{
					Console.WriteLine(" - {0}", user.Username);
				}
			}
        }
    }
}
