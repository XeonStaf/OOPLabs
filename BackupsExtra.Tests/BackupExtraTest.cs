using System;
using System.Collections.Generic;
using System.IO;
using Backups.Models;
using Backups.Services;
using Backups.SpecificRepository;
using Backups.SpecificSaveAlgorithm;
using BackupsExtra.PointsChooseAlgorithm;
using BackupsExtra.Services;
using Newtonsoft.Json;
using NUnit.Framework;

namespace BackupsExtra.Tests
{
 public class Tests
 {
     private IExtraBackupManager _extraBackupManager;
     private IBackupManager _backupManager;

     [SetUp]
        public void Setup()
        {
            _extraBackupManager = new ExtraBackupManager();
            _backupManager = new BackupManager();
            var dir = new DirectoryInfo(@"../../../../Backups/FileBackup");
            if (dir.Exists)
                dir.Delete(true);   
            Directory.CreateDirectory(@"../../../../Backups/FileBackup");
            Directory.CreateDirectory(@"../../../../Backups/FileBackup/Test1");
            Directory.CreateDirectory(@"../../../../Backups/FileBackup/Test2");            
        }

        [Test]
        public void ChoseRestorePointToDelete_FindAll()
        {
            DateTime dateLimit = DateTime.Now.AddDays(10);
            BackupJob job = _backupManager.AddBackupJob(new SplitStorages(), 
                new FileRepository(@"../../../../Backups/FileBackup/Test1/"));
            job.AddFile(@"../../../../Backups/testFile.txt");
            RestorePoint rp1 = job.CreateRestorePoint();
            job.RemoveFile(@"Backups/testFile3.txt");
            RestorePoint rp2 = job.CreateRestorePoint();
            RestorePoint rp3 = job.CreateRestorePoint();
            List<RestorePoint> result = _extraBackupManager.GetPointsToDelete(job, new DateDependAlgorithm(), dateLimit);
            Assert.AreEqual(result.Count, 3);
            Assert.Contains(rp1, result);
            Assert.Contains(rp2, result);
            Assert.Contains(rp3, result);

        }
        
        [Test]
        public void MergingPoint_DeleteAllStorages()
        {
            BackupJob job = _backupManager.AddBackupJob(new SingleStorage(), 
                new FileRepository(@"../../../../Backups/FileBackup/Test1/"));
            job.AddFile(@"../../../../Backups/testFile.txt");
            RestorePoint rp1 = job.CreateRestorePoint();
            job.RemoveFile(@"Backups/testFile3.txt");
            RestorePoint rp2 = job.CreateRestorePoint();
            _extraBackupManager.MergePoints(job, new List<RestorePoint>() {rp1}, rp2);
            Assert.AreEqual(0, rp1.Storages.Count);
            Assert.AreEqual(1, rp2.Storages.Count);
        }
        
        [Test]
        public void MergingPoints_AddStorageInNewPoint()
        {
            BackupJob job = _backupManager.AddBackupJob(new SplitStorages(), 
                new FileRepository(@"../../../../Backups/FileBackup/Test1/"));
            job.AddFile(@"../../../../Backups/testFile.txt");
            job.AddFile(@"../../../../Backups/testFile3.txt");
            RestorePoint rp1 = job.CreateRestorePoint();
            job.RemoveFile(@"Backups/testFile3.txt");
            RestorePoint rp2 = job.CreateRestorePoint();
            _extraBackupManager.MergePoints(job, new List<RestorePoint>() {rp1}, rp2);
            Assert.AreEqual(1, rp1.Storages.Count);
            Assert.AreEqual(2, rp2.Storages.Count);
        }

        [Test]
        public void SerializeJobObject_DeserializeJobObject()
        {
            BackupJob backupJob = _backupManager.AddBackupJob(new SplitStorages(), 
                new FileRepository(@"../../../../Backups/FileBackup/Test1/"));
            backupJob.AddFile(@"../../../../Backups/testFile.txt");
            backupJob.AddFile(@"../../../../Backups/testFile3.txt");
            var repository = new BackupJobRepository();
            repository.Save(backupJob);
            
            BackupJob newBackupJob = repository.GetBackupJob(backupJob.Key);
            Assert.AreEqual(backupJob.Key, newBackupJob.Key);
            Assert.AreEqual(backupJob.Repository.GetType(), newBackupJob.Repository.GetType());
            Assert.AreEqual(backupJob.SaveAlgorithm.GetType(), newBackupJob.SaveAlgorithm.GetType());
        }

    }
}