using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Backups.Models;

namespace Backups.SpecificSaveAlgorithm
{
    public class SplitStorages : SaveAlgorithm
    {
        public override List<Storage> CreateStorages(List<JobObject> jobObjects, int key)
        {
            var result = new List<Storage>();
            foreach (JobObject jobObject in jobObjects)
            {
                var sourceStream = new FileStream(jobObject.JobFile.FullName, FileMode.OpenOrCreate);
                string name = jobObject.JobFile.Name;
                name = name[..name.LastIndexOf('.')];
                name += $"_{key}.zip";
                string path = $@"../../../../Backups/FileBackup/{name}";
                using Stream targetStream = File.Create(@path);
                using var compressionStream = new GZipStream(targetStream, CompressionMode.Compress);
                sourceStream.CopyTo(compressionStream);
                result.Add(new Storage(new FileInfo(path)));
                sourceStream.Close();
                compressionStream.Close();
            }

            return result;
        }
    }
}