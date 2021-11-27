using System.IO;

namespace BackupsExtra.Logger
{
    public class FileLogger : MyCustomLogger
    {
        public FileLogger(bool isShowDate, string path)
            : base(isShowDate)
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