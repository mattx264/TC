using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace TC.BrowserEngine.Services
{
    public static class FileUploadService
    {
        public static async Task UploadScreenshotAsync(Byte[] file,string clientId, string guid)
        {
            try
            {
                var client = new HttpClient();
                var content = new MultipartFormDataContent();
                Stream stream = new MemoryStream(file);
                content.Add(new StreamContent(stream),"file","screenshot.png");
                content.Add(new StringContent(clientId), "clientId");
                content.Add(new StringContent(guid), "guid");
                var result = await client.PostAsync("https://localhost:44384/api/FileUpload/SaveScreenshot", content);
            }
            catch (Exception ex)
            {
                throw new Exception("Upload Image", ex);
            }
        }

    }
}
