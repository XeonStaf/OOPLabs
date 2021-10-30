using System;
using System.Collections.Generic;

namespace Backups.Models
{
    public class RestorePoint
    {
        public RestorePoint(int key, BackupJob backupJob, List<JobObject> jobObjects)
        {
            Created = DateTime.Now;
            Storages = new List<Storage>();
            Key = key;
            Storages = backupJob.SaveAlgorithm.CreateStorages(jobObjects, Key + 1);
            backupJob.Repository.SendFiles(Storages);
        }

        public int Key { get; }
        public List<Storage> Storages { get; }
        public DateTime Created { get; }
    }
}