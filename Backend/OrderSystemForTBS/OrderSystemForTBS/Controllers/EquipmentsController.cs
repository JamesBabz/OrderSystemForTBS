using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using BLL.BusinessObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OrderSystemForTBS.Controllers
{
    [EnableCors("MyPolicy")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize]
    public class EquipmentsController : Controller
    {
        private IBLLFacade _facade;

        public EquipmentsController(IBLLFacade facade)
        {
            _facade = facade;
        }

        // GET: api/Equipment/5
        [HttpGet("{id}")]
        public IEnumerable<EquipmentBO> Get(int Id)
        {
            return _facade.EquipmentService.GetAllById(Id);
        }

        // POST: api/Equipments
        [HttpPost]
        public IActionResult Post([FromBody]EquipmentBO equip)
        {
            //TODO modeltate is not used
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(_facade.EquipmentService.Create(equip));
        }

        // DELETE api/Equipment/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int Id)
        {
            try
            {
                return Ok(_facade.EquipmentService.Delete(Id));
            }
            catch (InvalidOperationException e)
            {
                return StatusCode(404, e.Message);
            }
        }
    }
}