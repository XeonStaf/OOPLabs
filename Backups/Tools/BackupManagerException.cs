using System;

namespace Backups.Tools
{
    public class BackupManagerException : Exception
    {
        public BackupManagerException()
        {
        }

        public BackupManagerException(string message)
            : base(message)
        {
        }

        public BackupManagerException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}