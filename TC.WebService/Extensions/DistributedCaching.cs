//using Microsoft.Extensions.Caching.Distributed;
//using System.Threading;
//using System.Threading.Tasks;

//namespace TC.WebService.Extensions
//{
//    public static class DistributedCaching
//    {
//        public async static Task SetAsync<T>(this IDistributedCache distributedCache, string key, T value, DistributedCacheEntryOptions options, CancellationToken token = default(CancellationToken))
//        {
//            var v = value.ToByteArray();
//            await distributedCache.SetAsync(key, v, options, token);
//        }

//        public async static Task<T> GetAsync<T>(this IDistributedCache distributedCache, string key, CancellationToken token = default(CancellationToken)) where T : class
//        {
//            var result = await distributedCache.GetAsync(key, token);
//            return result.FromByteArray<T>();
//        }
//    }
//}
