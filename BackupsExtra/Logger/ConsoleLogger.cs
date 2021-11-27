using System;

namespace BackupsExtra.Logger
{
    public class ConsoleLogger : MyLogger
    {
        public ConsoleLogger(bool isShowDate)
            : base(isShowDate)
        {
        }

        public override void WriteLine(string message)
        {
            Console.WriteLine($"{PrintDate()} {message}");
        }
    }
}