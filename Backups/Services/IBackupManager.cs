using System.Collections.Generic;
using Backups.Models;

namespace Backups.Services
{
    public interface IBackupManager
    {
        BackupJob AddBackupJob(SaveAlgorithm saveAlgorithm, Repository repository);
    }
}