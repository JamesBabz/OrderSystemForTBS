using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using BLL.BusinessObjects;
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
        public IEnumerable<EmployeeBO> Get()
        {
            return _facade.EmployeeService.GetAll();
        }

        // GET api/employee/5
        [HttpGet("{id}")]
        public EmployeeBO Get(int Id)
        {
            return _facade.EmployeeService.Get(Id);
        }

        // DELETE: api/employees/5
        [HttpDelete]
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

