using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reports.DAL.DataBase;
using Reports.DAL.Entities;

namespace Reports.Server.Services
{
    public class ReportService : IReportService
    {
        private ReportsDatabaseContext _context;

        public ReportService(ReportsDatabaseContext context)
        {
            _context = context;
        }
        
        public async Task<Report> Create(Guid employeeId, DateTime startDate)
        {
            var iEmployeeService = new EmployeeService(_context);
            var iTaskService = new TaskService(_context);
            Employee employee = iEmployeeService.FindById(employeeId).Result;
            var newReport = new Report(Guid.NewGuid(), employee);
            List<Employee> subordinates = iEmployeeService.GetSubordinates(employee).Result;
            subordinates.Add(employee);
            List<TaskModel> tasksBellowDate = iTaskService.FindByDate(startDate).Result;
            var taskByEmployee = new List<TaskModel>();
            
             subordinates.ForEach(e =>
             {
                 taskByEmployee.Add(iTaskService.FindByEmployee(e.Id).Result);
             });
             newReport.Tasks = taskByEmployee.AsQueryable().Intersect(tasksBellowDate).ToList();
            await _context.Reports.AddAsync(newReport);
            await _context.SaveChangesAsync();
            return newReport;
        }

        public async Task<bool> Close(Guid reportId)
        {
            Report report = _context.Reports.FirstOrDefaultAsync(r => r.Id == reportId).Result;
            if (report == default)
                return false;
            report.Closed = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Report>> GetAll()
        {
            var result = _context.Reports
                .Include("Employee")
                .Include("Tasks")
                .ToListAsync();

            result.Result.ForEach(r =>
            {
                r.Tasks.ForEach(t =>
                {
                    var findTask = t;
                    t = _context.Tasks.Include("AssignedEmployee").FirstOrDefault(task => task.Id == findTask.Id);
                });
            });
            
            return await result;
        }

        public async Task<Report> GetById(Guid reportId)
        {
            var report = _context.Reports.Include("Employee").Include("Tasks")
                .FirstOrDefaultAsync(r => r.Id == reportId);
            
            report.Result.Tasks.ForEach(t =>
            {
                var findTask = t;
                t = _context.Tasks.Include("AssignedEmployee").FirstOrDefault(task => task.Id == findTask.Id);
            });

            return await report;    
        }
    }
}