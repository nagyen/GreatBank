using System;
namespace console_app
{
    public abstract class BaseScreen
    {
        public void ShowInvalidOptionError()
        {
            Console.WriteLine("Invalid option. Please select valid option\n");
            Run();
        }

        public abstract void Run();
    }
}
