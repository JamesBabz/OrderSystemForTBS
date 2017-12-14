using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BLL;
using BLL.Services;
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
    public class CVRController : Controller
    {

        private IBLLFacade _facade;

        public CVRController(IBLLFacade facade)
        {
            _facade = facade;
        }
        
        // GET api/cvr/5
        [HttpGet("{query}")]
        public string[] Get(string query)
        {
            return _facade.CvrService.GetCompanyInfo(query);
        }


    }
}