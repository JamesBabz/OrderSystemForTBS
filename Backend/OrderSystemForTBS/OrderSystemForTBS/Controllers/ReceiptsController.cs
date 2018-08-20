using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BLL;
using BLL.BusinessObjects;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

namespace OrderSystemForTBS.Controllers
{ 

[EnableCors("MyPolicy")]
[Produces("application/json")]
[Route("api/[controller]")]
[Authorize(Roles = "Administrator, User")]
public class ReceiptsController : Controller
    {

        private IBLLFacade _facade;

        public ReceiptsController(IBLLFacade facade)
        {
            _facade = facade;
        }

        // GET api/receipts/5
        [HttpGet("{id}")]
        public IEnumerable<ReceiptBO> Get(int id)
        {
            return _facade.ReceiptService.GetAllById(id);
        }

        // POST api/Receipts
        [HttpPost]
        public IActionResult Post([FromBody]ReceiptBO receipt)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(_facade.ReceiptService.Create(receipt));
        }
        // PUT api/receipts/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ReceiptBO receipt)
        {
            if (id != receipt.Id)
            {
                return StatusCode(405, "Path id does not match customer ID json object");
            }
            try
            {
                return Ok(_facade.ReceiptService.Update(receipt));
            }
            catch (InvalidOperationException e)
            {
                return StatusCode(404, e.Message);
            }
        }

        // DELETE api/receipts/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            //TODO why modelState?
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(_facade.ReceiptService.Delete(id));
        }
    }
}