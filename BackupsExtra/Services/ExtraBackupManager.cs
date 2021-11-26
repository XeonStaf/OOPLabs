using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Models;
using Backups.Services;
using BackupsExtra.Logger;
using BackupsExtra.MergeRules;
using BackupsExtra.PointsChooseAlgorithm;
using BackupsExtra.RestoreAlgorithm;

namespace BackupsExtra.Services
{
    public class ExtraBackupManager : IExtraBackupManager
    {
        private MyLogger _myLogger;
        private IBackupManager _backupManager;
        public List<RestorePoint> GetPointsToDelete(
            BackupJob backupJob,
            IPointsChooseAlgorithm algorithm,
            object param1 = null,
            object param2 = null,
            object param3 = null)
        {
            return algorithm.ChoosePoints(backupJob, param1, param2, param3);
        }

        public void MergePoints(BackupJob backupJob, List<RestorePoint> restorePointsDelete, RestorePoint lastPoint)
        {
            var singleRule = new SingleStorageRule();
            var moveNewPoint = new MoveToNewPointRule();
            var oldCanDelete = new OldCanDeleteRule();
            singleRule.SetNext(moveNewPoint).SetNext(oldCanDelete);
            for (int i = 0; i < restorePointsDelete.Count - 1; i++)
                restorePointsDelete[i].Storages.ToList().ForEach(storage => singleRule.Handle(backupJob, restorePointsDelete[i], restorePointsDelete[i + 1], storage));

            restorePointsDelete[^1].Storages.ToList().ForEach(storage => singleRule.Handle(backupJob, restorePointsDelete[^1], lastPoint, storage));
        }

        public void SetLogger(MyLogger myLogger)
        {
            _myLogger = myLogger;
        }

        public RestorePoint CreateRestorePoint(BackupJob backupJob)
        {
            RestorePoint result = backupJob.CreateRestorePoint();
            _myLogger.WriteLine($"New Restore Point has been created and has key {result.Key}; {result.Storages.Count} archives generated");
            return result;
        }

        public BackupJob CreateBackupJob(IBackupManager backupManager, SaveAlgorithm saveAlgorithm, Repository repository)
        {
            _backupManager ??= backupManager;
            BackupJob result = backupManager.AddBackupJob(saveAlgorithm, repository);
            _myLogger.WriteLine($"New backup job has been created and has {saveAlgorithm.ToString()} algorithm in {repository}");
            return result;
        }

        public JobObject AddFileToJob(BackupJob backupJob, string path)
        {
            JobObject result = backupJob.AddFile(path);
            _myLogger.WriteLine($"File {result.JobFile.Name} added in backup job #{backupJob.Key}");
            return result;
        }

        public void RestoreFiles(BackupJob backupJob, RestorePoint restorePoint, string path = null)
        {
            Restore.RestoreFiles(backupJob, restorePoint, path);
            _myLogger.WriteLine($"Files from restore point #{restorePoint.Key} has been restored");
        }
    }
}