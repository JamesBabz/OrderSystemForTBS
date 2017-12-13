using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OrderSystemForTBS.Controllers
{
    using BLL;

    using Microsoft.AspNetCore.Cors;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;
    [EnableCors("MyPolicy")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class FilesController : Controller
    {
        private IBLLFacade facade;

        public FilesController(IBLLFacade facade)
        {
            this.facade = facade;
        }



      


        // POST: api/Customers
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]IFormFile files)
        {
            // Connect to Azure
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(this.facade.FileService.getConnectionString());
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("photos");

            // Save file to blob
            // Get a reference to a blob  
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(files.FileName);

            // Create or overwrite the blob with the contents of a local file 
            using (var fileStream = files.OpenReadStream())
            {
                await blockBlob.UploadFromStreamAsync(fileStream);
            }
            // Respond with success
            return Json(new
                            {
                                name = blockBlob.Name,
                                uri = blockBlob.Uri,
                                size = blockBlob.Properties.Length
                            });
        }
    }
}