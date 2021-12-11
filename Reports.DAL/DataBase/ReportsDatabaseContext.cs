using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Reports.DAL.Entities;


namespace Reports.DAL.DataBase
{
    public class ReportsDatabaseContext : DbContext
    {
        public ReportsDatabaseContext(DbContextOptions<ReportsDatabaseContext> options) : base(options)
        {
            // this.Database.EnsureDeleted();
            this.Database.EnsureCreated();
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<TaskModel> Tasks { get; set; }
        public DbSet<TaskChanges> Changes { get; set; }
        public DbSet<Report> Reports { get; set; }

        public string DbPath { get; private set; }

        public ReportsDatabaseContext()
        {
            DirectoryInfo? directoryInfo = new DirectoryInfo(Environment.CurrentDirectory).Parent;
            if (directoryInfo?.Parent == null) return;
            DirectoryInfo? folder = directoryInfo.Parent.Parent;
            DbPath = @$"{folder.FullName}/reports.db";
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("Employees");
            modelBuilder.Entity<TaskModel>().HasOne(model => model.AssignedEmployee);
            modelBuilder.Entity<TaskChanges>().HasOne(model => model.ChangedTask);
            modelBuilder.Entity<Report>().HasOne(model => model.Employee);
            modelBuilder.Entity<Report>().HasMany<TaskModel>(model => model.Tasks);
            base.OnModelCreating(modelBuilder);
        }
    }
}