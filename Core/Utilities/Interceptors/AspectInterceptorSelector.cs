using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

// Core.Utilities.Interceptors ad alanı içinde bir sınıf tanımlanıyor.
namespace Core.Utilities.Interceptors
{
    // AspectInterceptorSelector sınıfı, IInterceptorSelector arayüzünü uygular.
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        // SelectInterceptors metodu, bir tip (class), metod ve interceptor dizisi alır.
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            // Sınıfın üzerinde tanımlı olan MethodInterceptionBaseAttribute türündeki attribute'ları alır.
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>
                (true).ToList();
            
            // İlgili metodun üzerinde tanımlı olan MethodInterceptionBaseAttribute türündeki attribute'ları alır.
            var methodAttributes = type.GetMethod(method.Name)
                .GetCustomAttributes<MethodInterceptionBaseAttribute>(true);

            // Sınıf ve metod attribute'larını birleştirir.
            classAttributes.AddRange(methodAttributes);

            // Attribute'ları öncelik değerine göre sıralar ve dizi olarak geri döndürür.
            return classAttributes.OrderBy(x => x.Priority).ToArray();
        }
    }
}

