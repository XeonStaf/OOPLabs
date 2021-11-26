using System;

namespace BackupsExtra.Logger
{
    public class ConsoleLogger : MyLogger
    {
        public ConsoleLogger(bool showDate)
            : base(showDate)
        {
        }

        public override void WriteLine(string message)
        {
            Console.WriteLine($"{PrintDate()} {message}");
        }
    }
}