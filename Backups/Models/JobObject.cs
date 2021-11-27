using System;
using System.IO;
using Backups.Tools;

namespace Backups.Models
{
    [Serializable]
    public class JobObject
    {
        [NonSerialized]
        private FileInfo _jobFile;
        public JobObject(string path, BackupJob backupJob)
        {
            if (!File.Exists(path))
                throw new BackupManagerException("File does not Exists!");
            _jobFile = new FileInfo(path);
        }

        public FileInfo JobFile => _jobFile;
    }
}