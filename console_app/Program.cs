using System;
using core;
using core.DbModels;

namespace console_app
{
    class Program
    {
		public static void Main(string[] args)
		{
			// seed tables for demo
			core.SeedTables.Run();
            // show home screen
			new HomeScreen().Run();
		}
    }
}
