using System;
using System.IO.Compression;
using Backups.Models;
using Backups.SpecificSaveAlgorithm;

namespace BackupsExtra.RestoreAlgorithm
{
    public class Restore
    {
        public static void RestoreFiles(BackupJob backupJob, RestorePoint restorePoint, string path = null)
        {
            if (backupJob.SaveAlgorithm is SingleStorage && path == null)
                throw new Exception("You should enter the path, because you have used SingleStorage Algorythm");
            restorePoint.Storages.ForEach(storage =>
            {
                string zipPath = storage.Archive.FullName;
                if (path == null)
                {
                    JobObject file = backupJob.JobObjects.Find(job =>
                        job.JobFile.Name.Contains(storage.Archive.Name[..storage.Archive.Name.IndexOf('_')]));
                    if (file == null)
                        throw new Exception("Can't find original path");
                    Console.WriteLine(path);
                    path = file.JobFile.DirectoryName;
                }

                ZipFile.ExtractToDirectory(zipPath, path);
            });
        }
    }
}