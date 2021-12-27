using System;
using Reports.DAL.DataBase;
using Reports.DAL.Entities;

namespace Reports.DAL
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new ReportsDatabaseContext())
            {
                // Note: This sample requires the database to be created before running.
                Console.WriteLine($"Database path: {db.DbPath}");
                
                Console.WriteLine("Inserting a new blog");
                db.Add(new Employee(Guid.NewGuid(), "ivan", Guid.NewGuid()));
                db.SaveChanges();

            }
        }
    }
}