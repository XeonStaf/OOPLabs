using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Backups.Models;

namespace Backups.SpecificSaveAlgorithm
{
    [Serializable]
    public class SingleStorage : SaveAlgorithm
    {
        public override List<Storage> CreateStorages(List<JobObject> jobObjects, int key)
        {
            var result = new List<Storage>();
            string date = DateTime.Now.ToString();
            date = date.Replace('/', '-')[..date.IndexOf(' ')];
            string name = $"FullBackup_{date}_{key}.zip";
            string path = $"../../../../Backups/FileBackup/{name}";

            using (var fileStream = new FileStream(@path, FileMode.CreateNew))
            {
                using (var archive = new ZipArchive(fileStream, ZipArchiveMode.Create, true))
                {
                    foreach (JobObject jobObject in jobObjects)
                    {
                        var source = new FileStream(jobObject.JobFile.FullName, FileMode.Open);
                        ZipArchiveEntry zipArchiveEntry = archive.CreateEntry(jobObject.JobFile.Name, CompressionLevel.Fastest);
                        using Stream zipStream = zipArchiveEntry.Open();
                        source.CopyTo(zipStream);
                    }
                }
            }

            result.Add(new Storage(new FileInfo(path)));
            return result;
        }
    }
}