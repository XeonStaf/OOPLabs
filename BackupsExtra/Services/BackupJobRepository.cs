using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Backups.Models;

namespace BackupsExtra.Services
{
    public class BackupJobRepository : IBackupJobRepository
    {
        private string Path { get; } = @"../../../../BackupsExtra/";
        public void Save(BackupJob backupJob)
        {
            FileStream stream = File.Create($"{Path}/BackupJob_{backupJob.Key}.dat");
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, backupJob);
            stream.Close();
        }

        public BackupJob GetBackupJob(int key)
        {
            var dir = new DirectoryInfo(Path);
            var formatter = new BinaryFormatter();
            FileInfo[] files = dir.GetFiles($"BackupJob_{key}.dat");
            if (files.Length == 0)
                throw new Exception("Can't find file with this key");
            FileStream stream = File.OpenRead(files[0].FullName);
            var backupJob = formatter.Deserialize(stream) as BackupJob;
            return backupJob;
        }
    }
}