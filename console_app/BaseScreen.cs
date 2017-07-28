using System;
namespace console_app
{
    public abstract class BaseScreen
    {
        public void ShowInvalidOptionError()
        {
            Console.WriteLine("Invalid option. Please select valid option\n");
            Run(false);
        }

        public abstract void Run(bool clearScreen = true);
    }
}
