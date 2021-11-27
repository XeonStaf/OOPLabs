using System;
using System.Collections.Generic;
using System.IO;

namespace Backups.Models
{
    [Serializable]
    public abstract class Repository
    {
        public abstract void SendFiles(List<Storage> archives);
    }
}