using System;
using System.Collections.Generic;
using System.IO;
using Backups.Models;

namespace Backups.SpecificRepository
{
    public class FileRepository : Repository
    {
        public FileRepository(string path)
        {
            Path = path;
        }

        public string Path { get; }
        public override void SendFiles(List<Storage> archives)
        {
            foreach (Storage archive in archives)
            {
                archive.Archive.CopyTo($"{Path}/{archive.Archive.Name}");
            }
        }
    }
}