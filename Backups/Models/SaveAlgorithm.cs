using System;
using System.Collections.Generic;

namespace Backups.Models
{
    [Serializable]
    public abstract class SaveAlgorithm
    {
        public abstract List<Storage> CreateStorages(List<JobObject> jobObjects, int key);
    }
}