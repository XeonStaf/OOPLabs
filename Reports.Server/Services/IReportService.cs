using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reports.DAL.Entities;

namespace Reports.Server.Services
{
    public interface IReportService
    {
        Task<Report> Create(Guid employeeId, DateTime startDate);
        Task<bool> Close(Guid reportId);
        Task<List<Report>> GetAll();
        Task<Report> GetById(Guid reportId);
    }
}