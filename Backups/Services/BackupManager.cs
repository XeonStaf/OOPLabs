using System.Collections.Generic;
using Backups.Models;

namespace Backups.Services
{
    public class BackupManager : IBackupManager
    {
        private int _backupJobCounter = 0;
        private List<BackupJob> _backupJobs = new List<BackupJob>();
        public BackupJob AddBackupJob(SaveAlgorithm saveAlgorithm, Repository repository)
        {
            var newObject = new BackupJob(_backupJobCounter + 1, saveAlgorithm, repository);
            _backupJobCounter += 1;
            _backupJobs.Add(newObject);
            return newObject;
        }
    }
}