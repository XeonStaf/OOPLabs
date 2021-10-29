using System.IO;

namespace Backups.Models
{
    public class Storage
    {
        public Storage(FileInfo archive)
        {
            Archive = archive;
        }

        public FileInfo Archive { get; }
    }
}