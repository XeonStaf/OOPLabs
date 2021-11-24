using System.Collections.Generic;
using Backups.Models;

namespace BackupsExtra.PointsChooseAlgorithm
{
    public interface IPointsChooseAlgorithm
    {
        List<RestorePoint> ChoosePoints(BackupJob backupJob, object param1, object param2 = null, object param3 = null);
    }
}