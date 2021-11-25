using Backups.Models;

namespace BackupsExtra.MergeRules
{
    public interface IHandler
    {
        IHandler SetNext(IHandler handler);
        RestorePoint Handle(BackupJob backupJob, RestorePoint origin, RestorePoint destination, Storage storage);
    }
}