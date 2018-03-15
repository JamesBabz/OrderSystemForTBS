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

        // GET api/Propositions/5
        [HttpGet("{id}")]
        public IEnumerable<PropositionBO> Get(int id)
        {
            return _facade.PropositionService.GetAllById(id);
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
        public IActionResult Put(int id, [FromBody] PropositionBO prop)
        {
            if (id != prop.Id)
            {
                return StatusCode(405, "Path id does not match customer ID json object");
            }
            try
            {
                return Ok(_facade.PropositionService.Update(prop));
            }
            catch (InvalidOperationException e)
            {
                return StatusCode(404, e.Message);
            }
        }

        // DELETE api/Propositions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            //TODO why modelState?
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(_facade.PropositionService.Delete(id));
        }
    }
}
