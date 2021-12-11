using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reports.DAL.Entities;

namespace Reports.Server.Services
{
    public interface IEmployeeService
    {
        Task<Employee> Create(string name, Guid bossId);

        Task<Employee> FindByName(string name);

        Task<Employee> FindById(Guid id);
        Task<List<Employee>> GetAllEmployee();
        Task<bool> Delete(Guid id);

        Task<Employee> Update(Guid id, string newName = null, Guid newBoss = default);
    }
}