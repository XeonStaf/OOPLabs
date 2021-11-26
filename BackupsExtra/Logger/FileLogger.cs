using System.IO;

namespace BackupsExtra.Logger
{
    public class FileLogger : MyLogger
    {
        public FileLogger(bool showDate, string path)
            : base(showDate)
        {
            File = new FileInfo(path);
        }

        public FileInfo File { get; }

        public override void WriteLine(string message)
        {
            StreamWriter sw = File.AppendText();
            sw.WriteLine($"{PrintDate()} {message}");
            sw.Close();
        }
    }
}