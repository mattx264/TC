using System.Collections.Generic;
using System.Threading.Tasks;
using TC.WebService.Models;

namespace TC.WebService.Services.Interface
{
    public interface ICacheService
    {
        public Task<List<SzwagierModel>> GetSzwagierModelAsync(string key);
        public List<string> SearchKeys(string searchKey);
        Task SetSzwagierModelAsync(string cacheKey, List<SzwagierModel> szwagierModels);
    }
}
