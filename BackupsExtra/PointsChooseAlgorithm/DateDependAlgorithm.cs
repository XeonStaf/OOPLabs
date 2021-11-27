using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Models;

namespace BackupsExtra.PointsChooseAlgorithm
{
    public class DateDependAlgorithm : IPointsChooseAlgorithm
    {
        public List<RestorePoint> ChoosePoints(
            BackupJob backupJob,
            object param1,
            object param2 = null,
            object param3 = null)
        {
            var maxDate = (DateTime)param1;
            if (backupJob == null)
                throw new Exception("BackupJob is null");
            IEnumerable<RestorePoint> buffer = backupJob.RestorePoints.Where(point => point.Created < maxDate);
            var result = buffer.ToList();
            return result;
        }
    }
}