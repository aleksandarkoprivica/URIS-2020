using System;
using EasyCaching.Core;

namespace UserService.Services
{
    public class RedisService : ICacheService
    {
        private IEasyCachingProvider _cachingProvider;
        private IEasyCachingProviderFactory _cachingProviderFactory;
        
        public RedisService(IEasyCachingProviderFactory cachingProviderFactory)
        {
            _cachingProviderFactory = cachingProviderFactory;
            _cachingProvider = _cachingProviderFactory.GetCachingProvider("redis1");
        }
        
        public void SetValue(string key, string value)
        {
            // We will use 1 day for now as duration
            _cachingProvider.Set(key, value, TimeSpan.FromDays(1));
        }

        public string GetValue(string key)
        {
            var item = _cachingProvider.Get<string>(key);
            return item.ToString();
        }

        public void Delete(string key)
        {
            _cachingProvider.Remove(key);
        }
    }
}