using System;

namespace BackupsExtra.Logger
{
    public class ConsoleLogger : MyCustomLogger
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