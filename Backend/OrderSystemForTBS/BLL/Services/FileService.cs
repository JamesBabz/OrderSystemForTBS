using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure;
using Microsoft.WindowsAzure; // Namespace for CloudConfigurationManager
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Blob; // Namespace for Blob storage types

namespace BLL.Services
{
    using System.IO;

    public class FileService

    {

        string storageConnectionString =
            "DefaultEndpointsProtocol=https;AccountName=thom953b;AccountKey=0VQ3Mi5N2NCA5IWykeZltouBC6h0Pn+DOfy7rNXZlLYW/K9NwbMHXmTgMp1eOdDvq5iYeHk0l3gM/j0i1J/lQQ==;EndpointSuffix=core.windows.net";

        private CloudBlobClient serviceClient;
        public FileService()
        {

         

            CloudStorageAccount account = CloudStorageAccount.Parse(storageConnectionString);
           serviceClient = account.CreateCloudBlobClient();


        }

     

        
        

        public string getConnectionString()
        {
            return this.storageConnectionString;
        }

    }
}
