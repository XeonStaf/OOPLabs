using System;

namespace BackupsExtra.Logger
{
    public abstract class MyLogger
    {
        public MyLogger(bool showDate)
        {
            ShowDate = showDate;
        }

        public bool ShowDate { get; }
        public abstract void WriteLine(string message);
        protected string PrintDate()
        {
            return ShowDate ? $"[{DateTime.Now:yyyy-M-d}]" : string.Empty;
        }
    }
}