using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Models;

namespace BackupsExtra.PointsChooseAlgorithm
{
    public class HybridAlgorithm : IPointsChooseAlgorithm
    {
        public List<RestorePoint> ChoosePoints(
            BackupJob backupJob,
            object param1,
            object param2 = null,
            object param3 = null)
        {
            if (param2 == null || param3 == null)
                throw new Exception("param2 and param3 should not be null!");
            char logic = (char)param1;
            var maxNumber = (int)param2;
            var maxDate = (DateTime)param3;
            var countDependAlgorithm = new CountDependAlgorithm();
            var dateDependAlgorithm = new DateDependAlgorithm();
            List<RestorePoint> dependNumber = countDependAlgorithm.ChoosePoints(backupJob, maxNumber);
            List<RestorePoint> dependDate = dateDependAlgorithm.ChoosePoints(backupJob, maxDate);

            List<RestorePoint> result = dependNumber;
            result = logic == '|' ? result.AsQueryable().Union(dependDate).ToList() : result.AsQueryable().Intersect(dependDate).ToList();
            return result;
        }
    }
}