using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TC.WebService.Services.Files;

namespace TC.WebService.Services
{
    public interface IFileManager
    {
        public Task<string> SaveFile(string imageBase64, string fileExtension);
        public Task<string> SaveFile(IFormFile formFile);
    }
    public class FileManager : IFileManager
    {
        private IFileStorageService _fileStorageService;

        public FileManager(IFileStorageService fileStorageService)
        {
            _fileStorageService = fileStorageService;
        }
        public async Task<string> SaveFile(string imageBase64,string fileExtension)
        {
            var bits = Convert.FromBase64String(imageBase64);
            string fileName = DateTime.Now.ToShortDateString() + RandomString(10, true) + fileExtension;
            var filePath = await _fileStorageService.StoreFileAsync(fileName, bits);

            return filePath;
        }
        public async Task<string> SaveFile(IFormFile formFile)
        {
           
            string fileName = DateTime.Now.ToShortDateString() + RandomString(10, true) + Path.GetExtension(formFile.FileName);
            var filePath = await _fileStorageService.StoreFileAsync(fileName, formFile.OpenReadStream());

            return filePath;
        }
        private string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
    }
}
