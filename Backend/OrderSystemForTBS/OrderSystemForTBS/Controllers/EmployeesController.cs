using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using BLL.BusinessObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace OrderSystemForTBS.Controllers
{
    [EnableCors("MyPolicy")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    
    public class EmployeesController : Controller
    {
        IBLLFacade _facade;

        public EmployeesController(IBLLFacade facade)
        {
            _facade = facade;
        }

        // POST: api/employee/
        [HttpPost]
       // [Authorize(Roles = "Administrator")]
        public IActionResult Post([FromBody] EmployeeBO employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(_facade.EmployeeService.Create(employee));
        }

        // GET api/employee
        [HttpGet]
        [Authorize(Roles = "Administrator, User")]
        public IEnumerable<EmployeeBO> Get()
        {
            return _facade.EmployeeService.GetAll();
        }

        // GET api/employee/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator, User")]
        public EmployeeBO Get(int Id)
        {
            return _facade.EmployeeService.Get(Id);
        }

        // PUT api/employee/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator, User")]
        public IActionResult Put(int id, [FromBody] EmployeeBO emp)
        {
            if (id != emp.Id)
            {
                return StatusCode(405, "Path id does not match employee ID json object");
            }
            try
            {
                    return Ok(_facade.EmployeeService.Update(emp));
            }
            catch (InvalidOperationException e)
            {
                return StatusCode(404, e.Message);
            }
        }

        // DELETE: api/employees/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public IActionResult Delete(int Id)
        {
            try
            {
                return Ok(_facade.EmployeeService.Delete(Id));
            }
            catch (InvalidOperationException e)
            {
                return StatusCode(404, e.Message);
            }
        }
    }
}

