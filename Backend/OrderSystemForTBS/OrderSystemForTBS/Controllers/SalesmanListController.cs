﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BLL;
using BLL.BusinessObjects;
using Microsoft.AspNetCore.Cors;

namespace OrderSystemForTBS.Controllers
{

    [EnableCors("MyPolicy")]
    [Produces("application/json")]
    [Route("api/SalesmanList")]
    public class SalesmanListController : Controller
    {
        private IBLLFacade _facade;

        public SalesmanListController(IBLLFacade facade)
        {
            _facade = facade;
        }

        // GET: api/Visits/5
        [HttpGet("{id}")]
        public IEnumerable<SalesmanListBO> Get(int Id)
        {
            return _facade.salesmanListService.GetAllById(Id);
        }
        // POST: api/Visits
        [HttpPost]
        public IActionResult Post([FromBody]SalesmanListBO BO)
        {
            //TODO You dont use modelstate
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(_facade.salesmanListService.Create(BO));

        }
    }
}