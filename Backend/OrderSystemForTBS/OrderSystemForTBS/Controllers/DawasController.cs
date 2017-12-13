using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OrderSystemForTBS.Controllers
{
    [EnableCors("MyPolicy")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class DawasController : Controller
    {

        private IBLLFacade _facade;

        public DawasController(IBLLFacade facade)
        {
            _facade = facade;
        }
        
        // GET api/dawas/5
        [HttpGet("{zipCode}")]
        public string Get(int zipCode)
        {
            return _facade.DawaService.GetCityFromZipCode(zipCode);
        }


    }
}