using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using Backups.Models;
using Backups.Services;
using Backups.SpecificRepository;
using Backups.SpecificSaveAlgorithm;
using BackupsExtra.Logger;
using BackupsExtra.RestoreAlgorithm;
using BackupsExtra.Services;

namespace BackupsExtra
{
    internal class Program
    {
        private static void Main()
        {
            var dir = new DirectoryInfo(@"../../../../Backups/FileBackup");
            var dir2 = new DirectoryInfo(@"/Users/xeonstaf/Desktop/Test567");
            if (dir.Exists)
                dir.Delete(true);
            if (dir2.Exists)
                dir2.Delete(true);
            Directory.CreateDirectory(@"../../../../Backups/FileBackup");
            Directory.CreateDirectory(@"/Users/xeonstaf/Desktop/Test567");

            MyCustomLogger customLogger = new FileLogger(true, "/Users/xeonstaf/Desktop/Test567/log.txt");

            // MyLogger logger = new ConsoleLogger(true);
            customLogger.WriteLine("Executing Program.Main");
            IExtraBackupManager extraBackupManager = new ExtraBackupManager();
            IBackupManager backupManager = new BackupManager();
            extraBackupManager.SetLogger(customLogger);

            BackupJob backupJob = extraBackupManager.CreateBackupJob(backupManager, new SingleStorage(), new FileRepository(@"/Users/xeonstaf/Desktop/Test567"));
            extraBackupManager.AddFileToJob(backupJob, @"../../../../Backups/testFile.txt");
            extraBackupManager.AddFileToJob(backupJob, @"../../../../Backups/testFile2.txt");
            JobObject jobObject = extraBackupManager.AddFileToJob(backupJob, @"../../../../Backups/testFile3.txt");
            RestorePoint restorePoint = extraBackupManager.CreateRestorePoint(backupJob);
            extraBackupManager.RestoreFiles(backupJob, restorePoint, @"/Users/xeonstaf/Desktop/Test567");
        }
    }
}
