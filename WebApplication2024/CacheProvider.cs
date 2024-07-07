using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace WebApplication2024
{
    public class CacheProvider : ICacheProvider
    {
        private static readonly SemaphoreSlim GetUsersSemaphore = new SemaphoreSlim(1, 1);
        private readonly IMemoryCache _cache;
        public CacheProvider(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }
        public async Task<IEnumerable<Guest>> GetCachedResponse()
        {
            try
            {
                return await GetCachedResponse(CacheKeys.Guests, GetUsersSemaphore);
            }
            catch
            {
                throw;
            }
        }
        private async Task<IEnumerable<Guest>> GetCachedResponse(string cacheKey, SemaphoreSlim semaphore)
        {
            bool isAvaiable = _cache.TryGetValue(cacheKey, out List<Guest> guests);
            if (isAvaiable) return guests;
            try
            {
                await semaphore.WaitAsync();
                isAvaiable = _cache.TryGetValue(cacheKey, out guests);
                if (isAvaiable) return guests;
                //currently using read-only JSON
                //guests = GuestService.GetEmployeesDeatilsFromDB();
                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                    SlidingExpiration = TimeSpan.FromMinutes(2),
                    Size = 1024,
                };
                _cache.Set(cacheKey, guests, cacheEntryOptions);
            }
            catch
            {
                throw;
            }
            finally
            {
                semaphore.Release();
            }
            return guests;
        }
    }
}
