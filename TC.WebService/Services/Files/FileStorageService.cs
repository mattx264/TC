using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Auth;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TC.WebService.Services.Files
{
    public interface IFileStorageService
    {
        Task<string> StoreFileAsync(string filename, byte[] image);
        Task<string> StoreFileAsync(string filename, Stream image);
    }
    public class FileStorageService : IFileStorageService
    {
        private readonly IConfiguration _config;

        public FileStorageService(IConfiguration config)
        {
            _config = config;
        }
        public async Task<string> StoreFileAsync(string fileName, byte[] image)
        {
            try
            {
                var blob = await SetupAzureStoreageAsync(fileName);

                await blob.UploadFromByteArrayAsync(image, 0, image.Length);

                return blob.SnapshotQualifiedStorageUri.PrimaryUri.AbsoluteUri;
            }
            catch (Exception ex)
            {
                throw new Exception("StoreFileAsync", ex);

            }

        }
        public async Task<string> StoreFileAsync(string fileName, Stream image)
        {
            try
            {
                var blob =await SetupAzureStoreageAsync(fileName);
                await blob.UploadFromStreamAsync(image);

                return blob.SnapshotQualifiedStorageUri.PrimaryUri.AbsoluteUri;
            }
            catch (Exception ex)
            {
                throw new Exception("StoreFileAsync", ex);

            }

        }
        private async Task<CloudBlockBlob> SetupAzureStoreageAsync(string fileName)
        {
            // Retrieve storage account information from connection string
            // How to create a storage connection string - http://msdn.microsoft.com/en-us/library/azure/ee758697.aspx
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_config["Azure:StorageConnectionString"].ToString());

            // Create a blob client for interacting with the blob service.
            var blobClient = storageAccount.CreateCloudBlobClient();
            var blobContainer = blobClient.GetContainerReference("screenshot");
            await blobContainer.CreateIfNotExistsAsync();

            // To view the uploaded blob in a browser, you have two options. The first option is to use a Shared Access Signature (SAS) token to delegate  
            // access to the resource. See the documentation links at the top for more information on SAS. The second approach is to set permissions  
            // to allow public access to blobs in this container. Comment the line below to not use this approach and to use SAS. Then you can view the image  
            // using: https://[InsertYourStorageAccountNameHere].blob.core.windows.net/webappstoragedotnet-imagecontainer/FileName 
            await blobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

            // Gets all Cloud Block Blobs in the blobContainerName and passes them to teh view
            List<Uri> allBlobs = new List<Uri>();
            foreach (IListBlobItem bloba in blobContainer.ListBlobs())
            {
                if (bloba.GetType() == typeof(CloudBlockBlob))
                    allBlobs.Add(bloba.Uri);
            }
            return blobContainer.GetBlockBlobReference(fileName);
        }
    }
}