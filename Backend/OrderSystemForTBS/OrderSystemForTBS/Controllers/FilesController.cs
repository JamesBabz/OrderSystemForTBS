using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace OrderSystemForTBS.Controllers
{
    using System.IO;

    using BLL;

    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;

    using static System.Net.Mime.MediaTypeNames;

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
        public async Task<IActionResult> Post([FromBody] string file)
        {


            // Connect to Azure
            string storageConnectionString =
                "DefaultEndpointsProtocol=https;AccountName=thom953b;AccountKey=0VQ3Mi5N2NCA5IWykeZltouBC6h0Pn+DOfy7rNXZlLYW/K9NwbMHXmTgMp1eOdDvq5iYeHk0l3gM/j0i1J/lQQ==;EndpointSuffix=core.windows.net";
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("photos");

            // Convert base 64 string to byte[]
            byte[] imageBytes = Convert.FromBase64String(file);
            
            // Save file to blob
            // Get a reference to a blob  

            CloudBlockBlob blockBlob = container.GetBlockBlobReference("dfklgmdfkogsdø.png");
            blockBlob.UploadFromByteArrayAsync(imageBytes, 0, imageBytes.Length);

            // Create or overwrite the blob with the contents of a local file 
//            using (var fileStream = files.OpenReadStream())
//            {
//                await blockBlob.UploadFromStreamAsync(fileStream);
//            }
            // Respond with success
            return Json(new
                            {
                                name = blockBlob.Name,
                                uri = blockBlob.Uri,
                                size = blockBlob.Properties.Length
                            });
        }
        
    }

    public class Photo
    {
        public string File { get; set; }

        public string FileName { get; set; }
    }
}