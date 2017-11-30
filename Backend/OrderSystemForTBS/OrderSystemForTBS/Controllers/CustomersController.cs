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
   // [Authorize]
    public class CustomersController : Controller
    {
        private IBLLFacade facade;

        public CustomersController(IBLLFacade facade)
        {
            this.facade = facade;
        }


        // GET: api/Customers
        [HttpGet]
        public IEnumerable<CustomerBO> Get()
        {
            return this.facade.CustomerService.GetAll();
        }

        // GET: api/Customers/5
        [HttpGet("{id}", Name = "Get")]
        public CustomerBO Get(int Id)
        {
            return this.facade.CustomerService.Get(Id);
        }
        
        // POST: api/Customers
        [HttpPost]
        public IActionResult Post([FromBody]CustomerBO cust)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }
            return this.Ok(this.facade.CustomerService.Create(cust));
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
                return Ok(facade.CustomerService.Update(cust));
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
                return Ok(facade.CustomerService.Delete(Id));
            }
            catch (InvalidOperationException e)
            {
                return StatusCode(404, e.Message);
            }
        }
    }
}
