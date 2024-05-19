using Core.Utilities.IoC;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace Core.CrossCuttingConcerns.Caching.Microsoft
{
    public class MemoryCacheManager : ICacheManager
    {
        //Adapter Pattern 

        IMemoryCache _memoryCache;

        public MemoryCacheManager()
        {
            
            _memoryCache = ServiceTool.ServiceProvider.GetService<IMemoryCache>();

        }


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
        }

        public bool IsAdd(string key)
        {
            return _memoryCache.TryGetValue(key,out _);
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {
            // MemoryCache nesnesinin EntriesCollection özelliğine erişim sağlamak için reflection kullanılıyor.
            var cacheEntriesCollectionDefinition = typeof(MemoryCache).GetProperty("EntriesCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            // EntriesCollection özelliğinden değeri dynamic olarak alıyoruz.
            var cacheEntriesCollection = cacheEntriesCollectionDefinition.GetValue(_memoryCache) as dynamic;

            // Cache'deki tüm girişleri tutacak bir liste oluşturuyoruz.
            List<ICacheEntry> cacheCollectionValues = new List<ICacheEntry>();

            // Cache koleksiyonundaki her bir öğe için döngü başlatıyoruz.
            foreach (var cacheItem in cacheEntriesCollection)
            {
                // Her bir cache öğesinin değerini alıyoruz.
                ICacheEntry cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null);

                // Alınan değeri listeye ekliyoruz.
                cacheCollectionValues.Add(cacheItemValue);
            }

            // Verilen pattern ile eşleşen anahtarları bulmak için regex kullanıyoruz.
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);

            // Regex'e göre eşleşen anahtarları buluyoruz ve bir listeye alıyoruz.
            var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString())).Select(d => d.Key).ToList();

            // Eşleşen anahtarlar için döngü başlatıyoruz.
            foreach (var key in keysToRemove)
            {
                // Eşleşen anahtarları cache'den kaldırıyoruz.
                _memoryCache.Remove(key);
            }

            /*
             Metot Açıklaması: 
             -----------------
             Bu fonksiyon, belirli bir desene uyan cache anahtarlarını bulmak ve bu anahtarları cache’den kaldırmak için kullanılır. 
             Reflection, MemoryCache nesnesinin özel EntriesCollection özelliğine erişmek için kullanılır. 
             Bu koleksiyon, cache’deki tüm girişleri içerir. Regex, belirtilen desenle eşleşen anahtarları bulmak için kullanılır ve eşleşen anahtarlar Remove metodu ile cache’den silinir. 
             Bu yöntem, özellikle belirli bir desenle başlayan, biten veya belirli bir deseni içeren tüm cache girişlerini kaldırmak istediğinizde kullanışlıdır.
             
             */
        }

    }
}
