using System.IO;
using Backups.Tools;

namespace Backups.Models
{
    public class JobObject
    {
        public JobObject(string path, BackupJob backupJob)
        {
            if (!File.Exists(path))
                throw new BackupManagerException("File does not Exists!");
            JobFile = new FileInfo(path);
        }

        public FileInfo JobFile { get; }
    }
}