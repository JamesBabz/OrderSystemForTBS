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
    //TODO maybe a new service connected to this controller
    [EnableCors("MyPolicy")]
    [Produces("application/json")]
    [Route("api/Login")]
    public class LoginController : Controller
    {
        IBLLFacade _facade;

        private EmployeeConverter _employeeConverter;

        public LoginController(IBLLFacade facade)
        {
            _facade = facade;
            _employeeConverter = new EmployeeConverter();
        }

        
        [HttpPost]
        public IActionResult Login([FromBody]LoginInput LoginInput)
        {
            var user = _facade.EmployeeService.GetAll().FirstOrDefault(u => u.Username == LoginInput.Username);
            
            // check if username exists
            if (user == null)
                return Unauthorized();

            // check if password is correct
            if (!VerifyPasswordHash(LoginInput.Password, user.PasswordHash, user.PasswordSalt))
                return Unauthorized();

            // Authentication successful
            return Ok(new
            {
                passwordreset = user.PasswordReset,
                id = user.Id,
                username = user.Username,
                token = GenerateToken(_employeeConverter.Convert(user)),
            });
        }
        
        // This method verifies that the password entered by a user corresponds to the stored
        // password hash for this user. The method computes a Hash-based Message Authentication
        // Code (HMAC) using the SHA512 hash function. The inputs to the computation is the
        // password entered by the user and the stored password salt for this user. If the
        // computed hash value is identical to the stored password hash, the password entered
        // by the user is correct, and the method returns true.
        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }
            return true;
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