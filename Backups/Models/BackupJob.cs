using System.Collections.Generic;
using System.Linq;

namespace Backups.Models
{
    public class BackupJob
    {
        private static int _restorePointCounter = 0;
        private List<JobObject> _jobObjects;
        public BackupJob(int key, SaveAlgorithm saveAlgorithm, Repository repository)
        {
            Key = key;
            SaveAlgorithm = saveAlgorithm;
            Repository = repository;
            RestorePoints = new List<RestorePoint>();
            _jobObjects = new List<JobObject>();
        }

        public int Key { get; }
        public SaveAlgorithm SaveAlgorithm { get; }
        public Repository Repository { get; }
        public List<RestorePoint> RestorePoints { get; }

        public RestorePoint CreateRestorePoint()
        {
            var newObject = new RestorePoint(_restorePointCounter, this, _jobObjects);
            _restorePointCounter += 1;
            RestorePoints.Add(newObject);
            return newObject;
        }

        public JobObject AddFile(string path)
        {
            var newObject = new JobObject(path, this);
            _jobObjects.Add(newObject);
            return newObject;
        }

        public void RemoveFile(string path)
        {
            foreach (JobObject jobObject in _jobObjects.Where(jobObject => jobObject.JobFile.FullName.Contains(path)))
            {
                _jobObjects.Remove(jobObject);
                return;
            }
        }
    }
}