using System;
using System.Collections.Generic;
using System.Linq;

namespace Backups.Models
{
    [Serializable]
    public class BackupJob
    {
        private static int _restorePointCounter = 0;

        public BackupJob(int key, SaveAlgorithm saveAlgorithm, Repository repository)
        {
            Key = key;
            SaveAlgorithm = saveAlgorithm;
            Repository = repository;
            RestorePoints = new List<RestorePoint>();
            JobObjects = new List<JobObject>();
        }

        public List<JobObject> JobObjects { get; }
        public int Key { get; }
        public SaveAlgorithm SaveAlgorithm { get; }
        public Repository Repository { get; }
        public List<RestorePoint> RestorePoints { get; }

        public RestorePoint CreateRestorePoint()
        {
            var newObject = new RestorePoint(_restorePointCounter, this, JobObjects);
            _restorePointCounter += 1;
            RestorePoints.Add(newObject);
            return newObject;
        }

        public JobObject AddFile(string path)
        {
            var newObject = new JobObject(path, this);
            JobObjects.Add(newObject);
            return newObject;
        }

        public void RemoveFile(string path)
        {
            foreach (JobObject jobObject in JobObjects.Where(jobObject => jobObject.JobFile.FullName.Contains(path)))
            {
                JobObjects.Remove(jobObject);
                return;
            }
        }
    }
}