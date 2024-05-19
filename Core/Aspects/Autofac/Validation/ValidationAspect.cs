using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac.Validation
{
    // ValidationAspect sınıfı, MethodInterception sınıfından türetilmiştir.
    public class ValidationAspect : MethodInterception
    {
        // _validatorType, doğrulayıcı tipini tutar.
        private Type _validatorType;

        // Yapıcı metot, doğrulayıcı tipini alır.
        public ValidationAspect(Type validatorType)
        {
            // Eğer verilen tip IValidator'dan türetilmemişse hata fırlatılır.
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new System.Exception("Bu bir doğrulama sınıfı değil");
            }

            // Doğrulayıcı tipi ayarlanır.
            _validatorType = validatorType;
        }

        // OnBefore metodu, metot çağrısı öncesinde çalışır.
        protected override void OnBefore(IInvocation invocation)
        {
            // Doğrulayıcı nesnesi yaratılır.(Reflection ile) (ProductValidator den nesne oluşturuldu )
            var validator = (IValidator)Activator.CreateInstance(_validatorType);
            
            // Doğrulayıcının temel tipinden jenerik argümanlar alınır ve entity tipi belirlenir.(ProductValidator sınıfının basetype'ında jeneric çalıştığı sınıfı bul)
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];
            
            // Çağrılan metotun argümanlarından entity tipine uyanlar seçilir.(Validator tipine eşit olan parametreleri bulur)
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);
            
            // Her bir entity için doğrulama yapılır.(Her bir parametre için doğrulama yapılır)
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }

}

