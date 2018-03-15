using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

namespace OrderSystemForTBS.Controllers
{
    using System.IO;
    

    using BLL;

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
    [Authorize]
    public class FilesController : Controller
    {
        private IBLLFacade _facade;

        // TODO remember to remove connectionString from the backend or make it safe
        // Connect to Azure
        static string storageConnectionString =
                "DefaultEndpointsProtocol=https;AccountName=thom953b;AccountKey=oRR2tpbn654CPAdG5kDqPN2jSVxzfI4nm8xTj4qwNsXNN8p4I/v24pY8bVjdlcMDEZVakYMlTPWnXr4hXp3MkQ==;EndpointSuffix=core.windows.net";

        // TODO remove to service class
        static CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageConnectionString);

        // TODO remove to service class
        static CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

        // TODO remove to service class
        // get the reference to the container where all images a storaged
        CloudBlobContainer container = blobClient.GetContainerReference("files");

        public FilesController(IBLLFacade facade)
        {
            _facade = facade;
        }

        [HttpGet]
        public IEnumerable<int> Get()
        {
            return _facade.PropositionService.allFileIds();
        }

        [HttpGet("{id}")]
        public async Task<string> Get(int id)
        {
            // TODO remove to service class
            CloudBlockBlob blockBlob = container.GetBlockBlobReference($"{id}.pdf");

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
                    return base64; // use the same return string
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

            // TODO remove to service class
            // get the max value from file ids from the propositions
            // sets the id to max value +1
            int id = _facade.PropositionService.allFileIds().Max() + 1;

            // Get a reference to a blob  
            CloudBlockBlob blockBlob = container.GetBlockBlobReference($"{id}.pdf");

            // Convert base 64 string to byte[]
            byte[] imageBytes = Convert.FromBase64String(file);

            // Save file to blob
          var done = blockBlob.UploadFromByteArrayAsync(imageBytes, 0, imageBytes.Length);
            return Ok(done);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                CloudBlockBlob blockBlob = container.GetBlockBlobReference($"{id}.pdf");

                return Ok(blockBlob.DeleteAsync());
            }
            catch (InvalidOperationException e)
            {
                return StatusCode(404, e.Message);
            }
        }
    }
}