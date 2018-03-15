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

    //TODO Fix  Maybe with a service class
    [EnableCors("MyPolicy")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class FilesController : Controller
    {
        private IBLLFacade _facade;

        private FileService _fileService;


        public FilesController(IBLLFacade facade)
        {
            _facade = facade;
            this._fileService = new FileService(facade);
        }

        [HttpGet]
        public IEnumerable<int> Get()
        {
            return _facade.PropositionService.allFileIds();
        }

        [HttpGet("{id}")]
        public Task<string> Get(int id)
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
                return Ok(_fileService.createFile(file));
            }
            catch (Exception e)
            {
                return StatusCode(404, e.Message);
            }
            
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                return Ok(this._fileService.deleteFile(id).DeleteAsync());
            }
            catch (InvalidOperationException e)
            {
                return StatusCode(404, e.Message);
            }
        }
    }
}