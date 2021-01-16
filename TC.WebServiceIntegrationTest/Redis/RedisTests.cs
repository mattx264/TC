using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TC.WebService.Models;
using Xunit;
using TC.WebService.Helpers;
using System.Text.Json;

namespace TC.WebServiceIntegrationTest.Redis
{
    public class RedisTests
    {
        private const string KEY = "KEYTEST";
        [Fact]
        public async Task ConnectionTestsAsync()
        {
            var exception = await Record.ExceptionAsync(async () => await GetDistributedCache().GetAsync(KEY));
            Assert.Null(exception);
        }

        [Fact]
        public async Task SaveStringAndGetAsync()
        {
            var value = "ValUE";
            await GetDistributedCache().SetStringAsync(KEY, value);
            var result = await GetDistributedCache().GetStringAsync(KEY);
            Assert.Equal(result, value);
        }

        [Fact]
        public async Task SzwagierModelTests_Can()
        {
            var szwagierModels = new List<SzwagierModel>()
            {
                new SzwagierModel()
                {
                    ConnectionId=Guid.NewGuid().ToString(),
                    Location="test location Mars",
                    Name="my name",
                    SzwagierType =SzwagierType.SzwagierBrowserExtension,
                    UserId = Guid.NewGuid().ToString()
                }
            };
            var modelJson = JsonSerializer.Serialize(szwagierModels);
            await GetDistributedCache().SetStringAsync(KEY, modelJson, new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(2) });

            var resultJson = await GetDistributedCache().GetStringAsync(KEY);
            var szwagierModelsResult = JsonSerializer.Deserialize<List<SzwagierModel>>(resultJson);
            Assert.Equal(szwagierModels.Count, szwagierModelsResult.Count);
        }

        [Fact]
        public async Task SzwagierModelTests()
        {
            var szwagierModels = new List<SzwagierModel>()
            {
                new SzwagierModel()
                {
                    ConnectionId=Guid.NewGuid().ToString(),
                    Location="test location Mars",
                    Name="my name",
                    SzwagierType =SzwagierType.SzwagierBrowserExtension,
                    UserId = Guid.NewGuid().ToString()
                }
            };
            await GetDistributedCache().SetAsync<List<SzwagierModel>>(KEY, szwagierModels, new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(2) });

            var szwagierModelsResult = await GetDistributedCache().GetAsync<List<SzwagierModel>>(KEY);

            Assert.Equal(szwagierModels.Count, szwagierModelsResult.Count);
        }

        

        private IDistributedCache GetDistributedCache()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = "localhost";

            });
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            return serviceProvider.GetService<IDistributedCache>();
        }
    }
}
