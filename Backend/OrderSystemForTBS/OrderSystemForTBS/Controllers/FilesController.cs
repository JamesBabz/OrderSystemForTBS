using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace OrderSystemForTBS.Controllers
{
    using System.IO;
    

    using BLL;
    using BLL.Services;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;

    using NuGet.Frameworks;
    
    [EnableCors("MyPolicy")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrator, User")]
    public class FilesController : Controller
    {
        private FileService _fileService;

        public FilesController(IBLLFacade facade)
        {
            _fileService = new FileService();
        }

      

        [HttpGet("{id}")]
        public Task<string> Get(long id)
        {
            try
            {
                 return _fileService.GetFile(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }          
        }

        // POST: api/Customers
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string file)
        {
            try
            {
                return Ok(_fileService.CreateFile(file));
            }
            catch (Exception e)
            {
                return StatusCode(404, e.Message);
            }
            
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            try
            {
                return Ok(_fileService.DeleteFile(id).DeleteAsync());
            }
            catch (InvalidOperationException e)
            {
                return StatusCode(404, e.Message);
            }
        }
    }
}