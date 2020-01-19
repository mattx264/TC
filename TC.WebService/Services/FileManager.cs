using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC.WebService.Services.Files;

namespace TC.WebService.Services
{
    public interface IFileManager
    {
        public Task<string> SaveFile(string imageBase64);
    }
    public class FileManager : IFileManager
    {
        private IFileStorageService _fileStorageService;

        public FileManager(IFileStorageService fileStorageService)
        {
            _fileStorageService = fileStorageService;
        }
        public async Task<string> SaveFile(string imageBase64)
        {
            var bits = Convert.FromBase64String(imageBase64);
            string fileName = DateTime.Now.ToShortDateString() + RandomString(10, true) + ".jpg";
            var filePath =await _fileStorageService.StoreFileAsync(fileName, bits);

            return filePath;
        }

        public string RandomString(int size, bool lowerCase)
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
