using System;
using System.IO;
using Backups.Models;
using Backups.Services;
using Backups.SpecificRepository;
using Backups.SpecificSaveAlgorithm;
using NUnit.Framework;


namespace Backups.Tests
{
    public class Test
    {
        private IBackupManager _backupManager;
        
        [SetUp]
        public void Setup()
        {
            _backupManager = new BackupManager();
            var dir = new DirectoryInfo(@"../../../../Backups/FileBackup");
            if (dir.Exists)
                dir.Delete(true);
            Directory.CreateDirectory(@"../../../../Backups/FileBackup");
            Directory.CreateDirectory(@"../../../../Backups/FileBackup/Test1");
            Directory.CreateDirectory(@"../../../../Backups/FileBackup/Test2");
        }
        
        [Test]
        public void Add3File_Contains3Storages_DeleteFile_Contains2Storages()
        {
            BackupJob job = _backupManager.AddBackupJob(new SplitStorages(), 
                new FileRepository(@"../../../../Backups/FileBackup/Test1/"));
            job.AddFile(@"../../../../Backups/testFile.txt");
            job.AddFile(@"../../../../Backups/testFile2.txt");
            job.AddFile(@"../../../../Backups/testFile3.txt");
            RestorePoint restorePoint = job.CreateRestorePoint();
            Assert.AreEqual(3, restorePoint.Storages.Count);
            job.RemoveFile(@"Backups/testFile3.txt");
            RestorePoint newRestorePoint = job.CreateRestorePoint();

            Assert.AreEqual(2, newRestorePoint.Storages.Count);
        }

        [Test]
        public void Add3File_Contains1Storage()
        {
            BackupJob job = _backupManager.AddBackupJob(new SingleStorage(), 
                new FileRepository(@"../../../../Backups/FileBackup/Test2/"));
            job.AddFile(@"../../../../Backups/testFile.txt");
            job.AddFile(@"../../../../Backups/testFile2.txt");
            job.AddFile(@"../../../../Backups/testFile3.txt");
            RestorePoint restorePoint = job.CreateRestorePoint();
            Assert.AreEqual(1, restorePoint.Storages.Count);
        }
    }
}