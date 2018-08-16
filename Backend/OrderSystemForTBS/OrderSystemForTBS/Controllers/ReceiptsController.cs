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
    }
}