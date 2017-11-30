using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using BLL.BusinessObjects;
using BLL.Facade;
using Microsoft.AspNetCore.Mvc;

namespace OrderSystemForTBS.Controllers
{
    using Microsoft.AspNetCore.Cors;

    [EnableCors("MyPolicy")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PropositionsController : Controller
    {
        private IBLLFacade _facade;

        public PropositionsController(IBLLFacade facade)
        {
            _facade = facade;
        }

        // GET api/Propositions
        [HttpGet]
        public IEnumerable<PropositionBO> Get()
        {
            return _facade.PropositionService.GetAll();
        }

        // GET api/Propositions/cust/5
        [HttpGet("{id}")]
        public IEnumerable<PropositionBO> Get(int id)
        {
            return _facade.PropositionService.GetAllByCustomerId(id);
        }
        

        // POST api/Propositions
        [HttpPost]
        public IActionResult Post([FromBody]PropositionBO prop)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(_facade.PropositionService.Create(prop));
        }

        // PUT api/Propositions/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/Propositions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
