using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheAspect : MethodInterception
    {
        // Cache süresini tutacak özel bir alan.
        private int _duration;

        // Cache yönetiminden sorumlu olan ICacheManager tipinde bir alan.
        private ICacheManager _cacheManager;

        // Yapıcı metot, varsayılan olarak 60 saniye süre ile başlatılır.
        public CacheAspect(int duration = 60)
        {
            // Süreyi ayarla.
            _duration = duration; 
            // ServiceTool kullanarak ICacheManager servisini al ve _cacheManager alanına ata.
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        // Intercept metodu, herhangi bir metot çağrıldığında devreye girer.
        public override void Intercept(IInvocation invocation)
        {
            // Çağrılan metotun tam adını oluştur.
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");

            // Metot argümanlarını bir listeye dönüştür.
            var arguments = invocation.Arguments.ToList();

            // Cache anahtarını, metot adı ve argümanları kullanarak oluştur.
            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})";

            // Eğer cache'de bu anahtar varsa, cache'den değeri al ve metot çağrısını sonlandır.
            if (_cacheManager.IsAdd(key))
            {
                invocation.ReturnValue = _cacheManager.Get(key);
                return;
            }

            // Eğer cache'de bu anahtar yoksa, metot çağrısını sürdür.
            invocation.Proceed();

            // Metot çağrısının sonucunu, belirlenen süre ile cache'e ekle.
            _cacheManager.Add(key, invocation.ReturnValue, _duration);
        }
    }

}
