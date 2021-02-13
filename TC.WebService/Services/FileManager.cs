using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TC.WebService.Services.Files;
using TC.WebService.Services.Interface;

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
            string fileName = DateTime.Now.ToString("yyyy-MM-dd") + Guid.NewGuid() + fileExtension;
            var filePath = await _fileStorageService.StoreFileAsync(fileName, bits);

            return filePath;
        }
        public async Task<string> SaveFile(IFormFile formFile)
        {
           
            string fileName = DateTime.Now.ToString("yyyy-MM-dd") + Guid.NewGuid() + Path.GetExtension(formFile.FileName);
            var filePath = await _fileStorageService.StoreFileAsync(fileName, formFile.OpenReadStream());

            return filePath;
        }       
    }
}
