using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//erişim anahtarı
namespace Core.Utilities.Security.JWT
{
    public class AccessToken
    {
        // Token özelliği, erişim token'ını temsil eder. Bu token, bir kullanıcının
        // yetkilendirildiğini ve belirli kaynaklara erişim hakkı olduğunu gösterir.
        // '?' işareti ile nullable olarak tanımlanmıştır, yani bu özellik null değer alabilir.
        public string? Token { get; set; }

        // Expiration özelliği, token'ın geçerlilik süresinin ne zaman sona ereceğini belirtir.
        // Bu tarih ve saat bilgisi, token'ın kullanım süresini sınırlamak için kullanılır.
        public DateTime Expiration { get; set; }
    }

}

/*
 
Bu kod parçası, bir erişim token’ı (AccessToken) sınıfını tanımlar. AccessToken sınıfı, genellikle kullanıcıların kimlik doğrulama sonrası sistemdeki kaynaklara erişimlerini sağlayan bir token ve bu token’ın ne zaman geçersiz olacağını belirten bir son kullanma tarihi içerir. 
Token özelliği, kullanıcının oturum açtıktan sonra aldığı ve API isteklerinde kullanacağı token’ı saklar. Expiration özelliği ise token’ın ne zaman geçersiz olacağını belirten tarih ve saat bilgisini tutar. 
Bu bilgiler, genellikle bir API’nin güvenliğini sağlamak ve kullanıcı oturumlarını yönetmek için kullanılır. string? ifadesi ile Token özelliğinin null olabileceği belirtilmiştir, bu da token’ın henüz atanmamış olabileceği veya bir hata durumunda null değer alabileceği anlamına gelir. 
 
 
 */
