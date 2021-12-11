using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reports.DAL;
using Reports.DAL.DataBase;
using Reports.DAL.Entities;
using Reports.Server.Controllers;

namespace Reports.Server.Services
{
    public class TaskService : ITaskService
    {
        private ReportsDatabaseContext _context;

        public TaskService(ReportsDatabaseContext context)
        {
            _context = context;
        }

        public async Task<TaskModel> Create(TaskState taskState, string content, Guid assignedEmployee)
        {
            Task<Employee> employee = _context.Employees.FirstAsync(e => e.Id == assignedEmployee);
            var task = new TaskModel(Guid.NewGuid(), taskState, content, employee.Result, DateTime.Now, DateTime.Now);
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<List<TaskModel>> GetAllTask()
        {
            return await _context.Tasks.Include("AssignedEmployee").ToListAsync();
        }
        
        public async Task<TaskModel> FindById(Guid id)
        {
            var task = _context.Tasks.Include("AssignedEmployee").FirstOrDefaultAsync(t => t.Id == id);
            return await task;
        }

        public async Task<List<TaskModel>> FindByDate(DateTime created)
        {
            Task<List<TaskModel>> task = _context.Tasks.Include("AssignedEmployee").Where(t => t.Created >= created).ToListAsync();
            return await task;
        }

        public async Task<TaskModel> FindByDateLastChange(DateTime changed)
        {
            Task<TaskModel> task = _context.Tasks.Include("AssignedEmployee").FirstAsync(t => t.LastChanges < changed);
            return await task;
        }

        public async Task<TaskModel> FindByEmployee(Guid employeeId)
        {
            Task<TaskModel> task = _context.Tasks.Include("AssignedEmployee").FirstAsync(t => t.AssignedEmployee.Id == employeeId);
            return await task;
        }
        
        public async Task<List<TaskModel>> FindByChanger(Guid employeeId)
        {
            Task<List<TaskModel>> changedTasks = _context.Changes.Include("ChangedTask")
                .Where(c => c.ChangedPersonId == employeeId).Select(t => t.ChangedTask).ToListAsync();
            return await changedTasks;
        }

        public void Delete(Guid id)
        {
            Task<TaskModel> task = _context.Tasks.Include("AssignedEmployee").FirstAsync(t => t.Id == id);
            _context.Tasks.Remove(task.Result);
            _context.SaveChangesAsync();
        }

        public async Task<TaskModel> Update(Guid id, Guid changer, TaskState newState = default, Guid newEmployee = default, string newContent = null)
        {
            Task<TaskModel> taskFromDb = FindById(id);
            TaskModel taskObject = taskFromDb.Result;
            var changes = new TaskChanges(Guid.NewGuid(), taskObject, taskObject.State, taskObject.Content, taskObject.AssignedEmployee.Id, newState, newContent, newEmployee, changer);
            if (newState != default)
                taskFromDb.Result.State = newState;
            if (newEmployee != default)
            {
                Task<Employee> employee = _context.Employees.FirstAsync(e => e.Id == newEmployee);
                taskFromDb.Result.AssignedEmployee = employee.Result;
            }
                
            if (newContent != null)
                taskFromDb.Result.Content = newContent;
            _context.Changes.Add(changes);
            await _context.SaveChangesAsync();
            return await taskFromDb;
        }
    }
}