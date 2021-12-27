using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reports.DAL.DataBase;
using Reports.DAL.Entities;
using Reports.Server.Controllers;

namespace Reports.Server.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ReportsDatabaseContext _context;

        public EmployeeService(ReportsDatabaseContext context)
        {
            _context = context;
        }
        public async Task<Employee> Create(string name, Guid bossId)
        {
            var employee = new Employee(Guid.NewGuid(), name, bossId);
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> FindByName(string name)
        {
            Task<Employee> employee = _context.Employees.FirstAsync(e => e.Name == name);
            return await employee;
        }

        public async Task<Employee> FindById(Guid id)
        {
            Task<Employee> employee = _context.Employees.FirstAsync(e => e.Id == id);
            return await employee;
        }

        public async Task<bool> Delete(Guid id)
        {
            var employee = _context.Employees.FirstAsync(e => e.Id == id);
            if (employee == null)  
            {  
                return false;  
            }  
            _context.Employees.Remove(employee.Result);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Employee> Update(Guid id, string newName = null, Guid newBoss = default)
        {
            Task<Employee> employeeFromDb = _context.Employees.FirstAsync(e => e.Id == id);
            if (newName != null)
                employeeFromDb.Result.Name = newName;
            if (newBoss != default)
                employeeFromDb.Result.BossId = newBoss;
            await _context.SaveChangesAsync();
            return await employeeFromDb;
        }

        public async Task<List<Employee>> GetAllEmployee()
        {
            return await _context.Employees.ToListAsync();
        }
        
        public async Task<List<Employee>> GetSubordinates(Employee employee)
        {
            Task<List<Employee>> employees = 
                _context.Employees.Where(e => e.BossId == employee.Id).ToListAsync();
            return await employees;
        }

    }
}