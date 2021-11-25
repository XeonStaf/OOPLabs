using System;
using System.Text.RegularExpressions;
using Backups.Models;
using Backups.SpecificSaveAlgorithm;

namespace BackupsExtra.MergeRules
{
    public class SingleStorageRule : AbstractHandler
    {
        public override RestorePoint Handle(BackupJob backupJob, RestorePoint origin, RestorePoint destination, Storage storage)
        {
            if (origin.Storages.Count == 1 & backupJob.SaveAlgorithm is SingleStorage)
            {
                origin.Storages.Clear();
                return origin;
            }
            else
            {
                return base.Handle(backupJob, origin, destination, storage);
            }
        }
    }
}