using Backups.Models;

namespace BackupsExtra.MergeRules
{
    public class AbstractHandler : IHandler
    {
        private IHandler _nextHandler;
        public IHandler SetNext(IHandler handler)
        {
            _nextHandler = handler;
            return handler;
        }

        public virtual RestorePoint Handle(BackupJob backupJob, RestorePoint origin, RestorePoint destination, Storage storage)
        {
            if (_nextHandler != null)
            {
                return _nextHandler.Handle(backupJob, origin, destination, storage);
            }
            else
            {
                return null;
            }
        }
    }
}