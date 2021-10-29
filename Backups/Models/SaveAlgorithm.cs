using System.Collections.Generic;

namespace Backups.Models
{
    public abstract class SaveAlgorithm
    {
        public abstract List<Storage> CreateStorages(List<JobObject> jobObjects, int key);
    }
}