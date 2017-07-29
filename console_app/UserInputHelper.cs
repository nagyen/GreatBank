using System;
namespace console_app
{
    public class UserInputHelper
    {
        public static string GetText()
        {
            return Console.ReadLine();
        }

        public static string GetPassword()
        {
			string pass = "";
			ConsoleKeyInfo key;

			do
			{
				key = Console.ReadKey(true);

				if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
				{
					pass += key.KeyChar;
					Console.Write("*");
				}
				else
				{
					if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
					{
						pass = pass.Substring(0, (pass.Length - 1));
						Console.Write("\b \b");
					}
				}
			}
			// Stops Receving Keys Once Enter is Pressed
			while (key.Key != ConsoleKey.Enter);

            return pass;
        }

        // read decimal user input
        public static decimal GetDecimal()
        {
            string input = "";
            bool decimalStarted = false;
			ConsoleKeyInfo key;

			do
			{
				key = Console.ReadKey(true);

				if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
				{
                    // accept only numbers
                    if (Decimal.TryParse(key.KeyChar+"", out decimal decVal))
					{
                        input += key.KeyChar;
                        Console.Write(key.KeyChar);
					}
                    // look for decimals
                    if (key.KeyChar == '.' && !decimalStarted)
                    {
						input += key.KeyChar;
						Console.Write(key.KeyChar);
                        decimalStarted = true;
                    }
				}
				else
				{
                    if (key.Key == ConsoleKey.Backspace && input.Length > 0)
					{
                        input = input.Substring(0, (input.Length - 1));
						Console.Write("\b \b");
					}
				}
			}
			// Stops Receving Keys Once Enter is Pressed
			while (key.Key != ConsoleKey.Enter);
            Console.WriteLine();

            // return final decimal value
            if(Decimal.TryParse(input, out decimal decimalVal))
            {
                return decimalVal;
            }
            return 0;
        }

    }
}
