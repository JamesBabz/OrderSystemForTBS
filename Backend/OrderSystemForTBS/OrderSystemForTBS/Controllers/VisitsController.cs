using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class VisitsController : Controller
    {
        private IBLLFacade _facade;

        public VisitsController(IBLLFacade facade)
        {
            _facade = facade;
        }

        // GET: api/Visits
        [HttpGet]
        public IEnumerable<VisitBO> Get()
        {
            return _facade.VisitService.GetAll();
        }

        // GET: api/Visits/5
        [HttpGet("{id}")]
        public IEnumerable<VisitBO> Get(int id)
        {
            return _facade.VisitService.GetAllById(id);
        }
        
        // POST: api/Visits
        [HttpPost]
        public IActionResult Post([FromBody]VisitBO visit)
        {
            //TODO You dont use modelstate
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(_facade.VisitService.Create(visit));

        }
        
        // PUT: api/Visits/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] VisitBO visit)
        {
            if (id != visit.Id)
            {
                return StatusCode(405, "Path id does not match customer ID json object");
            }
            try
            {
                return Ok(_facade.VisitService.Update(visit));
            }
            catch (InvalidOperationException e)
            {
                return StatusCode(404, e.Message);
            }
        }

        // DELETE: api/visits/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int Id)
        {
            try
            {
                return Ok(_facade.VisitService.Delete(Id));
            }
            catch (InvalidOperationException e)
            {
                return StatusCode(404, e.Message);
            }
        }
    }
}
