using FluentValidation;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidationException = FluentValidation.ValidationException;
/*
 
            

//Product sınıfından product nesnesi için doğrulama yapılacak dendi 
var contex = new ValidationContext<Product>(product);
            
//ProductValidator den nesne oluşturuldu 
ProductValidator prooductValidator = new ProductValidator();    
            
//ProductValidator kullanılarak Fluent Api yazdığımız kurallar kontrol edildi (contex = product)
var result = prooductValidator.Validate(contex); 


ValidationContext sınıfı, FluentValidation kütüphanesinde doğrulama işlemleri sırasında kullanılır ve doğrulama yapılacak nesne ile ilgili bağlam bilgilerini taşır. Bu sınıfın işlevleri şunlardır:

Doğrulama Bağlamı Sağlama: Doğrulama yapılacak nesneye ilişkin bağlamı oluşturur ve doğrulama kurallarının uygulanmasını sağlar.
Ek Bilgi Aktarımı: Doğrulama işlemine ek bilgiler eklemek için kullanılır. Örneğin, doğrulama kurallarını belirli bir duruma göre uygulamak istediğinizde bu bilgileri sağlayabilirsiniz.
Özelleştirilmiş Doğrulama: Doğrulama işlemini özelleştirmek ve daha karmaşık senaryolara uyarlamak için kullanılabilir.
RootContextData Özelliği: RootContextData özelliği, doğrulama işlemine geçirilecek key-value çiftlerini içeren bir sözlük yapısıdır ve doğrulama sırasında nesne dışındaki verilere erişim sağlamak için kullanılabilir1.
Özetle, ValidationContext sınıfı, doğrulama işlemlerini daha esnek ve bağlama duyarlı hale getirmek için kullanılan önemli bir araçtır. 
Bu sınıf sayesinde, doğrulayıcılarınızı daha karmaşık ihtiyaçlara göre özelleştirebilir ve doğrulama sürecini genişletip derinleştirebilirsiniz2.        
 

ValidationContext sınıfını kullanmak, doğrulama işlemine ek bilgiler sağlamak ve daha karmaşık doğrulama senaryolarını desteklemek için tercih edilebilir. 
Örneğin, doğrulama kurallarını belirli bir bağlama göre uygulamak veya doğrulama sırasında ekstra parametreler kullanmak istiyorsanız, 
ValidationContext sınıfı bu tür durumlar için kullanışlı olacaktır. 
 */

// Core.CrossCuttingConcerns.Validation ad alanı içinde bir sınıf tanımlanıyor.
namespace Core.CrossCuttingConcerns.Validation
{
    // ValidationTool statik bir sınıftır, yani bu sınıftan nesne üretilemez ve doğrudan sınıf üzerinden erişilir.
    public static class ValidationTool
    {
        // Validate statik bir metoddur ve iki parametre alır: bir IValidator ve bir nesne (entity).
        public static void Validate(IValidator validator, object entity)
        {
            // ValidationContext nesnesi oluşturulur ve doğrulanacak nesne (entity) bu kontekste yerleştirilir.
            var context = new ValidationContext<object>(entity);

            // Validator üzerinden Validate metodu çağrılır ve sonuç bir ValidationResult nesnesinde saklanır.
            var result = validator.Validate(context);

            // Eğer sonuç geçerli değilse (IsValid özelliği false ise),
            // bir ValidationException fırlatılır ve hata mesajları bu istisnaya eklenir.
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}

