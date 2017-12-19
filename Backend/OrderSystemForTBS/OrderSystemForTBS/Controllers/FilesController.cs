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

    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;

    using NuGet.Frameworks;

    [EnableCors("MyPolicy")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize]
    public class FilesController : Controller
    {
        private IBLLFacade facade;

        // Connect to Azure
        static string storageConnectionString =
                "DefaultEndpointsProtocol=https;AccountName=thom953b;AccountKey=0VQ3Mi5N2NCA5IWykeZltouBC6h0Pn+DOfy7rNXZlLYW/K9NwbMHXmTgMp1eOdDvq5iYeHk0l3gM/j0i1J/lQQ==;EndpointSuffix=core.windows.net"
            ;

        static CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageConnectionString);

        static CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

        // get the reference to the container where all images a storaged
        CloudBlobContainer container = blobClient.GetContainerReference("photos");

        public FilesController(IBLLFacade facade)
        {
            this.facade = facade;
        }

        [HttpGet]
        public IEnumerable<int> Get()
        {
            return this.facade.PropositionService.allFileIds();
        }

        [HttpGet("{id}")]
        public async Task<string> Get(int id)
        {
            CloudBlockBlob blockBlob = container.GetBlockBlobReference($"{id}.png");

            // Sets all files on storage in the list
            var listBlobItems = await container.ListBlobsSegmentedAsync(
                                    string.Empty,
                                    true,
                                    BlobListingDetails.All,
                                    200,
                                    null,
                                    null,
                                    null);

            // Loops trough all files in storage
            foreach (var item in listBlobItems.Results)
            {
                // If it is the correct type and a file matchers the one I am looking for 
                if (item.GetType() == typeof(CloudBlockBlob) && item.Uri == blockBlob.Uri)
                {
                    // set the found item to a be 'blob'
                    CloudBlockBlob blob = (CloudBlockBlob)item;

                    // new bytearray with the lenght of the blob
                    byte[] b = new byte[blob.Properties.Length];

                    // download the file from storage to return a bytearray, that sets array 'b' and start at '0'
                    int done = await blob.DownloadToByteArrayAsync(b, 0);

                    // convert the byte[] to a base64 string, that is gonna be returned
                    string base64 = Convert.ToBase64String(b);
                    Console.WriteLine();
                    return base64;
                }
            }

            // if there is no matches it returns a emty string
            string emptyString = string.Empty;
            return emptyString;
        }

        // POST: api/Customers
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string file)
        {
            // get the max value from file ids from the propositions
            // sets the id to max value +1
            int id = this.facade.PropositionService.allFileIds().Max() + 1;

            // Get a reference to a blob  
            CloudBlockBlob blockBlob = container.GetBlockBlobReference($"{id}.png");

            // Convert base 64 string to byte[]
            byte[] imageBytes = Convert.FromBase64String(file);

            // Save file to blob
          var done = blockBlob.UploadFromByteArrayAsync(imageBytes, 0, imageBytes.Length);
            return this.Ok(done);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                CloudBlockBlob blockBlob = container.GetBlockBlobReference($"{id}.png");

                return this.Ok(blockBlob.DeleteAsync());
            }
            catch (InvalidOperationException e)
            {
                return StatusCode(404, e.Message);
            }
        }
    }
}