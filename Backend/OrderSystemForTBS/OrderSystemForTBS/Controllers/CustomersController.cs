using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OrderSystemForTBS.Controllers
{
    using BLL;
    using BLL.BusinessObjects;

    using Microsoft.AspNetCore.Cors;

    [EnableCors("MyPolicy")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize]
    public class CustomersController : Controller
    {
        private IBLLFacade _facade;

        public CustomersController(IBLLFacade facade)
        {
            _facade = facade;
        }

       
        [HttpGet]
        [Route("search")]
        public IEnumerable<CustomerBO> Search([FromQuery]string q)
        {
            if (q == null)
            {
                return Get();
            }
            var customers = _facade.CustomerService
                .GetAllBySearchQuery(q);
            return customers;
        }

        // GET: api/Customers
        [HttpGet]
        public IEnumerable<CustomerBO> Get()
        {
            return _facade.CustomerService.GetAll();
        }

        // GET: api/Customers/5
        [HttpGet("{id}", Name = "Get")]
        public CustomerBO Get(int Id)
        {
            return _facade.CustomerService.Get(Id);
        }
        
        // POST: api/Customers
        [HttpPost]
        public IActionResult Post([FromBody]CustomerBO cust)
        {
            // TODO you dont use ModelState
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(_facade.CustomerService.Create(cust));
        }

        // PUT: api/customer/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CustomerBO cust)
        {
            if (id != cust.Id)
            {
                return StatusCode(405, "Path id does not match customer ID json object");
            }
            try
            {
                return Ok(_facade.CustomerService.Update(cust));
            }
            catch (InvalidOperationException e)
            {
                return StatusCode(404, e.Message);
            }
        }

        // DELETE api/customer/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int Id)
        {
            try
            {
                return Ok(_facade.CustomerService.Delete(Id));
            }
            catch (InvalidOperationException e)
            {
                return StatusCode(404, e.Message);
            }
        }
    }
}
