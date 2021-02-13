using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TC.WebService.Services.Interface;

namespace TC.WebService.Services.Files
{
    public class FileStorageService : IFileStorageService
    {
        private IHostingEnvironment _hostingEnvironment;
        private IConfiguration _configuration;

        public FileStorageService(IHostingEnvironment hostingEnvironment, IConfiguration configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
        }

        public async Task<string> StoreFileAsync(string filename, byte[] image)
        {
            var fullPath = GetFullPath(filename);
            try
            {
                using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                {
                    await fs.WriteAsync(image, 0, image.Length);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in process: {0}", ex);
            }
            var url = _configuration["App:Url"];
            return $"{url}/screenshots/{filename}";
        }

        public async Task<string> StoreFileAsync(string filename, Stream image)
        {

            var fullPath = GetFullPath(filename);
            using (var fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
            {
                await image.CopyToAsync(fileStream);
            }
            var url = _configuration["App:Url"];
            return $"{url}/screenshots/{filename}";
        }

        private string GetFullPath(string filename)
        {
            string projectRootPath = _hostingEnvironment.ContentRootPath;
            return $"{projectRootPath}/wwwroot/screenshots/{filename}";
        }
    }
}
