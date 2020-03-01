using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using TC.BrowserEngine.Helpers;

namespace TC.BrowserEngine.Services
{
    public static class FileUploadService
    {
        public static async Task UploadScreenshotAsync(ScreenshotTestProgress screenshotTestProgress)
        {
            try
            {
                var client = new HttpClient();
                var content = new MultipartFormDataContent();
                Stream stream = new MemoryStream(screenshotTestProgress.Screenshot.AsByteArray);
                content.Add(new StreamContent(stream),"file","screenshot.png");
                content.Add(new StringContent(screenshotTestProgress.senderConnectionId), "clientId");
                content.Add(new StringContent(screenshotTestProgress.command.Guid), "guid");
                content.Add(new StringContent(screenshotTestProgress.TestRunHistoryId.ToString()), "testRunHistoryId");
                var result = await client.PostAsync($"{ConfigHelper.GetServerAddress()}api/FileUpload/SaveScreenshot", content);
            }
            catch (Exception ex)
            {
                throw new Exception("Upload Image", ex);
            }
        }

    }
}
