using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//security key anahtarını oluşturan sınıf 
namespace Core.Utilities.Security.Encryption
{
    // SecurityKeyHelper sınıfı, güvenlik anahtarları ile ilgili yardımcı işlevleri sağlar.
    public class SecurityKeyHelper
    {
        // CreateSecurityKey metodu, verilen bir güvenlik anahtarını (string) alır ve
        // bu anahtarı byte dizisine çevirerek bir SymmetricSecurityKey nesnesi oluşturur.
        // Bu nesne, JWT (JSON Web Token) gibi güvenlik token'ları oluştururken kullanılır.
        public static SecurityKey CreateSecurityKey(string securityKey)
        {
            // UTF8.GetBytes metodu ile string tipindeki güvenlik anahtarı(Web API'de bulunan appsettings.json dosyasındaki SecurityKey) byte dizisine dönüştürülür.
            // SymmetricSecurityKey sınıfının yapıcı metoduna bu byte dizisi verilerek
            // simetrik bir güvenlik anahtarı nesnesi oluşturulur.
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }
    }
}
/*
 
Bu kod parçası, güvenlikle ilgili işlemlerde kullanılmak üzere simetrik bir güvenlik anahtarı oluşturmak için kullanılır. 
Özellikle, JWT token’ları oluştururken ve doğrularken bu tür bir anahtar gereklidir. SymmetricSecurityKey sınıfı, simetrik şifreleme anahtarlarını temsil eder ve bu anahtarlar, hem şifreleme hem de şifre çözme işlemlerinde kullanılır. 
Encoding.UTF8.GetBytes metodu, bir string’i byte dizisine çevirir, bu da SymmetricSecurityKey sınıfının yapıcı metoduna argüman olarak verilir. Bu şekilde, bir string’den güvenlik anahtarı oluşturulmuş olur.
 
 
 */
