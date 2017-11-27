using System;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;

using System.Linq;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using BLL;
using BLL.Converters;
using DAL.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.IdentityModel.Tokens;


namespace OrderSystemForTBS.Controllers
{
    [EnableCors("MyPolicy")]
    [Produces("application/json")]
    [Route("api/Login")]
    public class LoginController : Controller
    {
        IBLLFacade facade;
        private EmployeeConverter employeeConverter = new EmployeeConverter();

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
                password = user.Password,
                token = GenerateToken(employeeConverter.Convert(user)),             
            });
        }

        private string GenerateToken(Employee employee)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, employee.Username)
            };

            var token = new JwtSecurityToken(
                new JwtHeader(new SigningCredentials(
                    JwtSecurityKey.Key,
                    SecurityAlgorithms.HmacSha256)),
                new JwtPayload(null, // issuer - not needed (ValidateIssuer = false)
                    null, // audience - not needed (ValidateAudience = false)
                    claims.ToArray(),
                    DateTime.Now,               // notBefore
                    DateTime.Now.AddDays(1)));  // expires

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}