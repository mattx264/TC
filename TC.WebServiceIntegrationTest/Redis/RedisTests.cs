using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TC.WebService.Models;
using Xunit;
using TC.WebService.Extensions;
using System.Text.Json;
using TC.WebService;
using StackExchange.Redis;
using System.Linq;

namespace TC.WebServiceIntegrationTest.Redis
{
    public class RedisTests
    {
        private const string KEY = "KEYTEST";
        [Fact]
        public async Task ConnectionTestsAsync()
        {
            var exception = await Record.ExceptionAsync(async () => await GetDistributedCache().GetDatabase().StringGetAsync(KEY));
            Assert.Null(exception);
        }

        [Fact]
        public async Task SaveStringAndGetAsync()
        {
            var value = "ValUE";
            await GetDistributedCache().GetDatabase().StringSetAsync(KEY, value);
            var result = await GetDistributedCache().GetDatabase().StringGetAsync(KEY);
            Assert.Equal(result, value);
        }


        [Fact]
        public async Task WildcardSearchAsync()
        {
            var value = "random string";
            var randomkey = "cOs";
            randomkey += TestHelper.RandomString(5);
            await GetDistributedCache().GetDatabase().StringSetAsync(randomkey, value);

            await GetDistributedCache().GetDatabase().StringSetAsync(KEY, value);
            var server = GetDistributedCache().GetServer("localhost", 6379);
            // show all keys in database 0 that include "foo" in their name
            var returnValue = "";
            foreach (var key in server.Keys(pattern: "cOs*"))
            {
                returnValue = await GetDistributedCache().GetDatabase().StringGetAsync(key);
            }
            Assert.Equal(value, returnValue);
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
            await GetDistributedCache().GetDatabase().StringSetAsync(KEY, szwagierModels.ToByteArray());

            var resultJson = await GetDistributedCache().GetDatabase().StringGetAsync(KEY);
            var szwagierModelsResult = JsonSerializer.Deserialize<List<SzwagierModel>>(resultJson);
            Assert.Equal(szwagierModels.Count, szwagierModelsResult.Count);
            Assert.Equal(szwagierModels.First().Name, szwagierModelsResult.First().Name);
        }

        //[Fact]
        //public async Task SzwagierModelTests()
        //{
        //    var szwagierModels = new List<SzwagierModel>()
        //    {
        //        new SzwagierModel()
        //        {
        //            ConnectionId=Guid.NewGuid().ToString(),
        //            Location="test location Mars",
        //            Name="my name",
        //            SzwagierType =SzwagierType.SzwagierBrowserExtension,
        //            UserId = Guid.NewGuid().ToString()
        //        }
        //    };
        //    await GetDistributedCache().GetDatabase().set<List<SzwagierModel>>(KEY, szwagierModels, new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(2) });

        //    var szwagierModelsResult = await GetDistributedCache().GetAsync<List<SzwagierModel>>(KEY);

        //    Assert.Equal(szwagierModels.Count, szwagierModelsResult.Count);
        //}



        private IConnectionMultiplexer GetDistributedCache()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddRedisMultiplexer(() =>
               ConfigurationOptions.Parse("localhost"));
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            return serviceProvider.GetService<IConnectionMultiplexer>();
        }
    }
}
