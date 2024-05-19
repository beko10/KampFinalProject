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
    public class CacheRemoveAspect : MethodInterception
    {
        // Desen tutmak için kullanılan özel bir alan.
        private string _pattern;

        // Cache yönetiminden sorumlu olan ICacheManager tipinde bir alan.
        private ICacheManager _cacheManager;

        // Yapıcı metot, desen alır ve ICacheManager servisini alarak _cacheManager alanına atar.
        public CacheRemoveAspect(string pattern)
        {
            _pattern = pattern; // Deseni ayarla.
                                // ServiceTool kullanarak ICacheManager servisini al ve _cacheManager alanına ata.
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        // OnSuccess metodu, metot başarıyla tamamlandığında devreye girer.
        protected override void OnSuccess(IInvocation invocation)
        {
            // _pattern ile eşleşen tüm cache girişlerini kaldır.
            _cacheManager.RemoveByPattern(_pattern);
        }

        /*
         Bu sınıf, MethodInterception sınıfından türetilmiştir ve AOP (Aspect-Oriented Programming) prensiplerine göre çalışır. 
        OnSuccess metodu, hedef metot başarıyla tamamlandığında devreye girer ve belirtilen desenle eşleşen tüm cache girişlerini kaldırır. 
        Bu yaklaşım, özellikle bir işlem sonucunda ilgili cache girişlerini temizlemek istediğinizde kullanışlıdır. Örneğin, bir ürün güncellendiğinde, o ürüne ait cache girişlerini kaldırmak için kullanılabilir. 
        Böylece, sonraki isteklerde güncellenmiş verilerin alınması sağlanır.
         
         */
    }

}
