using Core.Utilities.IoC;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;
using System.Linq;

namespace Core.CrossCuttingConcerns.Caching.Microsoft
{
    public class MemoryCacheManager : ICacheManager
    {
        IMemoryCache _memoryCache;
        public MemoryCacheManager()
        {
            //IMemoryCache _memoryCache'i injectiondan almamız gerekiyyor
            _memoryCache = ServiceTool.ServiceProvider.GetService<IMemoryCache>();
            //CoreModule.cs'de serviceCollection.AddMemoryCache(); dediğimiz zaman arka planda bir instance oluşuyor bizim onu almamız gerekiyor
        }
        //benim derdim sadece microsoft'un memory cache 'ine eklemek değil
        //eğer onu ekelrsem yarın öbürgün başka cache yönetiminde patlarım 
        //gidip aspect içi ddahi olsa hard code içine yazarsam yarın patlarım
        // ben ne yapıyorum aslında bu dotnetcore'dan gelen kodları(metotları add get vs) kendime uyarlıyorum
        //Adapter pattern uyguluyoruz.
        public void Add(string key, object value, int duration)
        {
            _memoryCache.Set(key, value, TimeSpan.FromMinutes(duration));
        }

        public T Get<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }

        public object Get(string key)
        {
            return _memoryCache.Get(key);
            //tip belirtmedende gönderebiliyoruz ama tip dönüşümü yapmamız gerekiyor
            //referans aynı ama boxing yapmamız gerekiyor.
        }

        public bool IsAdd(string key)
        {
            return _memoryCache.TryGetValue(key, out _);
            //birşey döndürmek istemiyorsan out _ ile kullanıyorsun.

        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {
            var cacheEntriesCollectionDefinition = typeof(MemoryCache).GetProperty("EntriesCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var cacheEntriesCollection = cacheEntriesCollectionDefinition.GetValue(_memoryCache) as dynamic;
            List<ICacheEntry> cacheCollectionValues = new List<ICacheEntry>();
            foreach (var cacheItem in cacheEntriesCollection)
            {
                ICacheEntry cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null);
                cacheCollectionValues.Add(cacheItemValue);
            }
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString())).Select(d => d.Key).ToList();
            foreach (var key in keysToRemove)
            {
                _memoryCache.Remove(key);
            }
        }
    }
}
