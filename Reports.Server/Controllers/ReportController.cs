using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using Microsoft.AspNetCore.Mvc;
using Reports.DAL;
using Reports.DAL.Entities;
using Reports.Server.Services;

namespace Reports.Server.Controllers
{
    [ApiController]
    [Route("/task")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ReportController: ControllerBase
    {
        private readonly IReportService _service;
        
        public ReportController(IReportService service)
        {
            _service = service;
        }
        
        [HttpPost]
        [Route("/report/create")]
        public async Task<Report> Create([FromQuery][Required] Guid employeeId, [FromQuery][Required] DateTime startDate)
        {
            
            return await _service.Create(employeeId, startDate);
        }
        
        [HttpPost]
        [Route("/report/closed")]
        public async Task<IActionResult> Close([FromQuery][Required] Guid reportId)
        {
            return _service.Close(reportId).Result ? Ok() : StatusCode((int) HttpStatusCode.UnavailableForLegalReasons);
        }
        
        [HttpGet]
        [Route("/report/all")]
        public async Task<List<Report>> GetAll()
        {
            return await _service.GetAll();
        }

        [HttpGet]
        [Route("/report/get")]
        public async Task<Report> GetById([FromQuery][Required] Guid reportId)
        {
            return await _service.GetById(reportId);
        }
    }
}