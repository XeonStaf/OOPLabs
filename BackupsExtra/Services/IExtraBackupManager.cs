using System.Collections.Generic;
using Backups.Models;
using Backups.Services;
using BackupsExtra.Logger;
using BackupsExtra.PointsChooseAlgorithm;

namespace BackupsExtra.Services
{
    public interface IExtraBackupManager
    {
        public List<RestorePoint> GetPointsToDelete(
            BackupJob backupJob,
            IPointsChooseAlgorithm algorithm,
            object param1 = null,
            object param2 = null,
            object param3 = null);
        void MergePoints(BackupJob backupJob, List<RestorePoint> restorePointsDelete, RestorePoint lastPoint);
        void SetLogger(MyCustomLogger myCustomLogger);
        RestorePoint CreateRestorePoint(BackupJob backupJob);
        BackupJob CreateBackupJob(IBackupManager backupManager, SaveAlgorithm saveAlgorithm, Repository repository);
        JobObject AddFileToJob(BackupJob backupJob, string path);
        void RestoreFiles(BackupJob backupJob, RestorePoint restorePoint, string path = null);
    }
}