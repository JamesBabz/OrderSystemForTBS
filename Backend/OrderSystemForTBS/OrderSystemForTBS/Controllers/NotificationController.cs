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
public class NotificationController : Controller
    {

        private IBLLFacade _facade;

        public NotificationController(IBLLFacade facade)
        {
            _facade = facade;
        }

        // GET api/receipts/5
        [HttpGet("{id}")]
        public IEnumerable<ReceiptBO> Get(int id, DateTime date)
        {
            return _facade.ReceiptService.GetNotificationList(id, date);
        }
    }
}