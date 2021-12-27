using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Reports.DAL;
using Reports.DAL.Entities;
using Reports.Server.Services;

namespace Reports.Server.Controllers
{
    [ApiController]
    [Route("/task")]
    public class TaskChangesController : ControllerBase
    {
        private readonly ITaskChangesService _service;
        
        public TaskChangesController(ITaskChangesService service)
        {
            _service = service;
        }
        
        [HttpGet]
        [Route("/task/getAllChanges")]
        public async Task<List<TaskChanges>> GetAll()
        {
            return await _service.GetAllChanges();
        }
    }
}