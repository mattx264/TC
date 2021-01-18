using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TC.WebService.Models;
using TC.WebService.Services.Interface;
using TC.WebService.Extensions;

namespace TC.WebService.Services
{
    public class CacheService : ICacheService
    {
        private IDistributedCache _distributedCache;

        public CacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }
        public async Task<List<SzwagierModel>> GetSzwagierModelAsync(string key)
        {
            return await _distributedCache.GetAsync<List<SzwagierModel>>(key);

        }
    }
}
