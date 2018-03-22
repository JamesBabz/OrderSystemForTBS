using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;

    public class FileService
    {
        // Connect to Azure
        static string storageConnectionString =
            "DefaultEndpointsProtocol=https;AccountName=thom953b;AccountKey=oRR2tpbn654CPAdG5kDqPN2jSVxzfI4nm8xTj4qwNsXNN8p4I/v24pY8bVjdlcMDEZVakYMlTPWnXr4hXp3MkQ==;EndpointSuffix=core.windows.net";

        static CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageConnectionString);

        static CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

        // get the reference to the container where all images a storaged
        CloudBlobContainer container = blobClient.GetContainerReference("files");

        public bool CreateFile(string fileString)
        {
            String[] stringarr = fileString.Split("å");
            fileString = stringarr[0];
            long timeStamp =  Int64.Parse( stringarr[1]);

            // Get a reference to a blob  
            CloudBlockBlob blockBlob = container.GetBlockBlobReference($"{timeStamp}.pdf");

            // Convert base 64 string to byte[]
            byte[] imageBytes = Convert.FromBase64String(fileString);

            // Save file to blob
            blockBlob.UploadFromByteArrayAsync(imageBytes, 0, imageBytes.Length);
            return true;
        }

        public async Task<string> GetFile(long id)
        {
            CloudBlockBlob blockBlob = container.GetBlockBlobReference($"{id}.pdf");

            String returnString = "";

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
                    await blob.DownloadToByteArrayAsync(b, 0);

                    // convert the byte[] to a base64 string, that is gonna be returned
                     returnString = Convert.ToBase64String(b);
                }
            }
            return returnString;
        }

        public CloudBlockBlob DeleteFile(long id)
        {
            return container.GetBlockBlobReference($"{id}.pdf");
        }
    }


}
