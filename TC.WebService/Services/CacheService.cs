using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TC.WebService.Models;
using TC.WebService.Services.Interface;

namespace TC.WebService.Services
{
    public class CacheService : ICacheService
    {
        private IConnectionMultiplexer _connectionMultiplexer;

        public CacheService(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;

        }
        public async Task<List<SzwagierModel>> GetSzwagierModelAsync(string key)
        {
            var resultJson = await _connectionMultiplexer.GetDatabase().StringGetAsync(key);
            if (!resultJson.HasValue)
            {
                return null;
            }
            var szwagierModelsResult = JsonSerializer.Deserialize<List<SzwagierModel>>(resultJson);
            return szwagierModelsResult;
        }

        public List<string> SearchKeys(string searchKey)
        {
            //TODO replace "localhost" with appconfig
            var server = _connectionMultiplexer.GetServer("localhost", 6379);
            return server.Keys(pattern: searchKey).Select(x => x.ToString()).ToList();
        }

        public async Task SetSzwagierModelAsync(string key, List<SzwagierModel> szwagierModels)
        {
            if (szwagierModels is null)
            {
                throw new ArgumentNullException(nameof(szwagierModels));
            }

            var modelJson = JsonSerializer.Serialize(szwagierModels);
            await _connectionMultiplexer.GetDatabase().StringSetAsync(key, modelJson);
        }
    }
}
