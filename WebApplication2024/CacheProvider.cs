using Microsoft.Extensions.Caching.Memory;
using NHibernate.Cache;
using LazyCache;
using ICacheProvider = NHibernate.Cache.ICacheProvider;

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

        public ICache BuildCache(string regionName, IDictionary<string, string> properties)
        {
            throw new NotImplementedException();
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

        public long NextTimestamp()
        {
            throw new NotImplementedException();
        }

        public void Start(IDictionary<string, string> properties)
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        private async Task<IEnumerable<Guest>> GetCachedResponse(string cacheKey, SemaphoreSlim semaphore)
        {
            bool isAvailable = _cache.TryGetValue(cacheKey, out List<Guest> guests);
            if (isAvailable) return guests;
            try
            {
                await semaphore.WaitAsync();
                isAvailable = _cache.TryGetValue(cacheKey, out guests);
                if (isAvailable) return guests;
                //currently using read-only JSON 
                //guests = GuestService.GetGuestsDetailsFromDB();
                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                    SlidingExpiration = TimeSpan.FromMinutes(2),
                    Size = 1024
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
