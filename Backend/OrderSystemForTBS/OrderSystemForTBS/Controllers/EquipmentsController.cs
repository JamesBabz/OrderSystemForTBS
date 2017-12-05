using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using BLL.BusinessObjects;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OrderSystemForTBS.Controllers
{
    [EnableCors("MyPolicy")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class EquipmentsController : Controller
    {
        private IBLLFacade facade;

        public EquipmentsController(IBLLFacade facade)
        {
            this.facade = facade;
        }


        // GET: api/Equipment
        [HttpGet]
        public IEnumerable<EquipmentBO> Get()
        {
            return this.facade.EquipmentService.GetAll();
        }

        // GET: api/Equipment/5
        [HttpGet("{id}")]
        public IEnumerable<EquipmentBO> Get(int Id)
        {
            return facade.EquipmentService.GetAllById(Id);
        }

        // POST: api/Equipments
        [HttpPost]
        public IActionResult Post([FromBody]EquipmentBO equip)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }
            return this.Ok(this.facade.EquipmentService.Create(equip));
        }

        // PUT: api/Equipment/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] EquipmentBO equip)
        {
            if (id != equip.id)
            {
                return StatusCode(405, "Path id does not match customer ID json object");
            }
            try
            {
                return Ok(facade.EquipmentService.Update(equip));
            }
            catch (InvalidOperationException e)
            {
                return StatusCode(404, e.Message);
            }
        }

        // DELETE api/Equipment/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int Id)
        {
            try
            {
                return Ok(facade.EquipmentService.Delete(Id));
            }
            catch (InvalidOperationException e)
            {
                return StatusCode(404, e.Message);
            }
        }
    }
}