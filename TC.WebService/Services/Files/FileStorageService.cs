using Microsoft.Extensions.Configuration;
using Azure.Storage.Blobs;
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
                using (MemoryStream memory = new MemoryStream(image))
                {
                    var blob = await SetupAzureStoreageAsync(fileName);
                    await blob.UploadAsync(memory);

                    // return blob.SnapshotQualifiedStorageUri.PrimaryUri.AbsoluteUri;
                    //this is new - have to be tested
                    return blob.Uri.AbsoluteUri;
                }

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
                var blob = await SetupAzureStoreageAsync(fileName);
                await blob.UploadAsync(image);

                // return blob.SnapshotQualifiedStorageUri.PrimaryUri.AbsoluteUri;
                //this is new - have to be tested
                return blob.Uri.AbsoluteUri;
            }
            catch (Exception ex)
            {
                throw new Exception("StoreFileAsync", ex);
            }

        }
        private async Task<BlobClient> SetupAzureStoreageAsync(string fileName)
        {
            var connectionString = _config["Azure:StorageConnectionString"].ToString();
            BlobContainerClient container = new BlobContainerClient(connectionString, "screenshot");
            await container.CreateIfNotExistsAsync();

            // Get a reference to a blob named "sample-file" in a container named "sample-container"
            return container.GetBlobClient(fileName);           
        }
    }
}