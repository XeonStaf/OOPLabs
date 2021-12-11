using System.Collections.Generic;
using System.Threading.Tasks;
using Reports.DAL.Entities;

namespace Reports.Server.Services
{
    public interface ITaskChangesService
    {
        Task<List<TaskChanges>> GetAllChanges();
    }
}