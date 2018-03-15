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

        //TODO is this useed?

        // GET api/employee/5
        [HttpGet("{id}")]
        public EmployeeBO Get(int Id)
        {
            return _facade.EmployeeService.Get(Id);
        }
    }
}

