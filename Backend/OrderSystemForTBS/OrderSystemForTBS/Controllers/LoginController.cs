using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using DAL.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OrderSystemForTBS.Controllers
{
    [EnableCors("MyPolicy")]
    [Produces("application/json")]
    [Route("api/Login")]
    public class LoginController : Controller
    {
        IBLLFacade facade;

        public LoginController(IBLLFacade facade)
        {
            this.facade = facade;
        }

        
        [HttpPost]
        public IActionResult Login([FromBody]LoginInput LoginInput)
        {
            var user = facade.EmployeeService.GetAll().FirstOrDefault(u => u.Username == LoginInput.Username);

            // check if username exists
            if (user == null)
                return Unauthorized();

            // check if password is correct
            if (user.Password != LoginInput.Password)
                return Unauthorized();

            // Authentication successful
            return Ok(new
            {
                username = user.Username,
                password = user.Password
            });
        }
    }
}