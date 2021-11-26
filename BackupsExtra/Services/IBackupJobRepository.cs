using Backups.Models;

namespace BackupsExtra.Services
{
    public interface IBackupJobRepository
    {
        void Save(BackupJob backupJob);
        BackupJob GetBackupJob(int key);
    }
}