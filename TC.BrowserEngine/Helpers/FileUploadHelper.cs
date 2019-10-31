using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace TC.BrowserEngine.Helpers
{
    public class FileUploadHelper
    {
        private IConfiguration config;

        public FileUploadHelper()
        {
            config= Program.SetupConfig();
        }
        public async Task<bool> UploadImageAsync(Stream image, string fileName)
        {
            
             HttpContent fileStreamContent = new StreamContent(image);
            fileStreamContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data") { Name = "file", FileName = fileName };
            fileStreamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
            using (var client = new HttpClient())
            using (var formData = new MultipartFormDataContent())
            {
                formData.Add(fileStreamContent);
                var response = await client.PostAsync(config["serverUrl"]+ "/api/Image", formData);
                return response.IsSuccessStatusCode;
            }
        }
    }
}
