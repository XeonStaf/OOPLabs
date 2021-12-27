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
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _service;
        
        public TaskController(ITaskService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("/task/add")]
        public async Task<TaskModel> Create([FromQuery][Required] TaskState taskState, [FromQuery][Required] string content, [FromQuery][Required] Guid employeeId)
        {
            return await _service.Create(taskState, content, employeeId);
        }
        
        [HttpGet]
        [Route("/task/findById")]
        public async Task<TaskModel> FindById([FromQuery][Required] Guid id)
        {
            return await _service.FindById(id);
        }
        
        [HttpGet]
        [Route("/task/findByDate")]
        public async Task<List<TaskModel>> FindById([FromQuery][Required] DateTime created)
        {
            return await _service.FindByDate(created);
        }
        
        [HttpGet]
        [Route("/task/findByLastChanged")]
        public async Task<TaskModel> FindByLastChanged([FromQuery][Required] DateTime changed)
        {
            return await _service.FindByDateLastChange(changed);
        }
        
        [HttpGet]
        [Route("/task/findByEmployee")]
        public async Task<TaskModel> FindByEmployee([FromQuery][Required] Guid employeeId)
        {
            return await _service.FindByEmployee(employeeId);
        }
        
        [HttpGet]
        [Route("/task/findByChanger")]
        public async Task<List<TaskModel>> FindByChanger([FromQuery][Required] Guid employeeId)
        {
            return await _service.FindByChanger(employeeId);
        }
        
        [HttpGet]
        [Route("/task/all")]
        public async Task<List<TaskModel>> GetAllTask()
        {
            return await _service.GetAllTask();
        }

        [HttpPost]
        [Route("/task/update")]
        public async Task<IActionResult> Update([FromQuery][Required] Guid id, [FromQuery] TaskState newState, [FromQuery] Guid newEmployee, [FromQuery] string newContent)
        {
            if (id == Guid.Empty && (newState == default || newEmployee == default || newContent == null)) return StatusCode((int) HttpStatusCode.BadRequest);
            return Ok(_service.Update(id, Guid.NewGuid(), newState, newEmployee, newContent));
        }
    }
    
    
}