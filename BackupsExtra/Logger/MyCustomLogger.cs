using System;

namespace BackupsExtra.Logger
{
    public abstract class MyCustomLogger
    {
        public MyCustomLogger(bool isShowDate)
        {
            IsShowDate = isShowDate;
        }

        public bool IsShowDate { get; }
        public abstract void WriteLine(string message);
        protected string PrintDate()
        {
            return IsShowDate ? $"[{DateTime.Now:yyyy-M-d}]" : string.Empty;
        }
    }
}