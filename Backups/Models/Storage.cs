using System;
using System.IO;

namespace Backups.Models
{
    [Serializable]
    public class Storage
    {
        [NonSerialized]
        private FileInfo _archive;
        public Storage(FileInfo archive)
        {
            _archive = archive;
        }

        public FileInfo Archive => _archive;
    }
}