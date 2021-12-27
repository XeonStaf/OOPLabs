using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Reports.DAL.Entities;
using Reports.Server.Services;
using System.Web.Http.Cors;

namespace Reports.Server.Controllers
{
    [ApiController]
    [Route("/employees")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }
    
        [HttpPost]
        [Route("/employees/add")]
        public async Task<Employee> Create([FromQuery][Required] string name, [FromQuery] Guid bossId)
        {
            return await _service.Create(name, bossId);
        }

        [HttpGet]
        [Route("/employees/get")]
        public async Task<IActionResult> Find([FromQuery] string name, [FromQuery] Guid id)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                Task<Employee> result = _service.FindByName(name);
                if (result != null)
                {
                    return Ok(result);
                }

                return NotFound();
            }

            if (id != Guid.Empty)
            {
                Task<Employee> result = _service.FindById(id);
                if (result != null)
                {
                    return Ok(result);
                }

                return NotFound();
            }

            return StatusCode((int) HttpStatusCode.BadRequest);
        }
        
        [HttpGet]
        [Route("/employee/all")]
        public async Task<List<Employee>> GetAll()
        {
            return await _service.GetAllEmployee();
        }
        
        [HttpPost]
        [Route("/employee/delete")]
        public async Task<IActionResult> Delete([FromQuery][Required] Guid id)
        {
            if (id == Guid.Empty) return StatusCode((int) HttpStatusCode.BadRequest);
            return _service.Delete(id).Result ? Ok() : StatusCode((int) HttpStatusCode.UnprocessableEntity);
        }      
        
        [HttpPost]
        [Route("/employee/update")]
        public async Task<IActionResult> Update([FromQuery][Required] Guid id, [FromQuery] string newName, [FromQuery] Guid newBoss)
        {
            if (id == Guid.Empty && (newName == null || newBoss == default)) return StatusCode((int) HttpStatusCode.BadRequest);
            return Ok(_service.Update(id, newName, newBoss));
        }   
    }
}