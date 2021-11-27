using Backups.Models;

namespace BackupsExtra.MergeRules
{
    public class OldCanDeleteRule : AbstractHandler
    {
        public override RestorePoint Handle(BackupJob backupJob, RestorePoint origin, RestorePoint destination, Storage storage)
        {
            string name = storage.Archive.Name[..storage.Archive.Name.IndexOf('_')];
            Storage oldStorage = origin.Storages.Find(storage => storage.Archive.Name.Contains(name));
            Storage newStorage = destination.Storages.Find(storage => storage.Archive.Name.Contains(name));

            if (oldStorage != null & newStorage != null)
            {
                origin.Storages.Remove(oldStorage);
                return origin;
            }
            else
            {
                return base.Handle(backupJob, origin, destination, storage);
            }
        }
    }
}