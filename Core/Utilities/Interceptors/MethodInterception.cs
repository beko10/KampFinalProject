using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// Core.Utilities.Interceptors ad alanı içinde bir sınıf tanımlanıyor.
namespace Core.Utilities.Interceptors
{
    // MethodInterception, MethodInterceptionBaseAttribute sınıfından türetilmiş soyut bir sınıftır.
    public abstract class MethodInterception : MethodInterceptionBaseAttribute
    {
        // OnBefore metodu, bir invocation (çağrı) öncesinde çalıştırılacak kodu içerir. 
        // Şu anda boş ve gerektiğinde ezilebilir (override).
        protected virtual void OnBefore(IInvocation invocation) { }

        // OnAfter metodu, bir invocation sonrasında çalıştırılacak kodu içerir.
        // Şu anda boş ve gerektiğinde ezilebilir.
        protected virtual void OnAfter(IInvocation invocation) { }

        // OnException metodu, bir invocation sırasında bir istisna (exception) oluştuğunda çalıştırılacak kodu içerir.
        // Şu anda boş ve gerektiğinde ezilebilir.
        protected virtual void OnException(IInvocation invocation, System.Exception e) { }

        // OnSuccess metodu, bir invocation başarıyla tamamlandığında çalıştırılacak kodu içerir.
        // Şu anda boş ve gerektiğinde ezilebilir.
        protected virtual void OnSuccess(IInvocation invocation) { }

        // Intercept metodu, bir invocation'ı kesme (intercept) işlemini gerçekleştirir.
        public override void Intercept(IInvocation invocation)
        {
            // isSuccess değişkeni, invocation'ın başarılı olup olmadığını takip eder.
            var isSuccess = true;

            // Invocation öncesinde OnBefore metodu çağrılır.
            OnBefore(invocation);

            try
            {
                // Invocation'ın asıl işlemi gerçekleştirilir.
                invocation.Proceed();
            }
            catch (Exception e)
            {
                // Bir istisna oluşursa, isSuccess false olarak ayarlanır ve OnException metodu çağrılır.
                isSuccess = false;
                OnException(invocation, e);

                // İstisna dışarı fırlatılır.
                throw;
            }
            finally
            {
                // Eğer isSuccess hala true ise, OnSuccess metodu çağrılır.
                if (isSuccess)
                {
                    OnSuccess(invocation);
                }
            }

            // Invocation sonrasında OnAfter metodu çağrılır.
            OnAfter(invocation);
        }
    }
}

