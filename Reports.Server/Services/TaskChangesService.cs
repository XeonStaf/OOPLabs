using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reports.DAL.DataBase;
using Reports.DAL.Entities;

namespace Reports.Server.Services
{
    public class TaskChangesService : ITaskChangesService
    {
        private ReportsDatabaseContext _context;

        public TaskChangesService(ReportsDatabaseContext context)
        {
            _context = context;
        }
        
        public async Task<List<TaskChanges>> GetAllChanges()
        {
            return await _context.Changes.Include("ChangedTask").ToListAsync();
        }
    }
}