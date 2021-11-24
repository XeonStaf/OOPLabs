using System;
using System.Collections.Generic;
using Backups.Models;

namespace BackupsExtra.PointsChooseAlgorithm
{
    public class CountDependAlgorithm : IPointsChooseAlgorithm
    {
        public List<RestorePoint> ChoosePoints(
            BackupJob backupJob,
            object param1,
            object param2 = null,
            object param3 = null)
        {
            int maxNumber = (int)param1;
            var result = new List<RestorePoint>();
            if (backupJob == null)
                throw new Exception("BackupJob is null");
            int restorePointsCount = backupJob.RestorePoints.Count;
            int toDelete = restorePointsCount - maxNumber;
            for (int i = 0; i < toDelete; i++)
                result.Add(backupJob.RestorePoints[i]);
            return result;
        }
    }
}