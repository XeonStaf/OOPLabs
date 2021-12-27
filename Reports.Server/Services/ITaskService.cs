using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reports.DAL;
using Reports.DAL.Entities;

namespace Reports.Server.Services
{
    public interface ITaskService
    {
        Task<TaskModel> Create(TaskState taskState, string content, Guid assignedEmployee);

        Task<TaskModel> FindById(Guid id);
        Task<List<TaskModel>> FindByDate(DateTime created);
        Task<TaskModel> FindByDateLastChange(DateTime changed);
        Task<TaskModel> FindByEmployee(Guid employeeId);
        Task<List<TaskModel>> FindByChanger(Guid employeeId);
        Task<List<TaskModel>> GetAllTask();
        void Delete(Guid id);

        Task<TaskModel> Update(Guid id, Guid changer, TaskState newState = default, Guid newEmployee = default, string newContent = null);
    }
}