using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Core.Utilities.Interceptors ad alanı içinde bir sınıf tanımlanıyor.
namespace Core.Utilities.Interceptors
{
    // AttributeUsage attribute'u bu attribute'un nerede kullanılabileceğini belirtir.
    // Class ve Method hedeflerine uygulanabilir, birden fazla kez kullanılabilir ve miras yoluyla geçebilir.
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]

    // MethodInterceptionBaseAttribute, Attribute ve IInterceptor'dan türetilmiş soyut bir sınıftır.
    public abstract class MethodInterceptionBaseAttribute : Attribute, IInterceptor
    {
        // Priority özelliği, aspect'lerin uygulanma önceliğini belirler.
        public int Priority { get; set; }

        // Intercept metodu, bir invocation'ı kesme (intercept) işlemini gerçekleştirir.
        // Şu anda boş bırakılmış ve ihtiyaca göre ezilebilir (override) durumdadır.
        public virtual void Intercept(IInvocation invocation)
        {

        }
    }
}
