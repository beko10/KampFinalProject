using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Encryption
{
    public class SigningCredentialsHelper
    {
        // CreateSigningCredentials metodu, bir güvenlik anahtarı alır ve bu anahtarı kullanarak
        // yeni bir SigningCredentials nesnesi oluşturur. Bu nesne, bir token imzalanırken
        // kullanılan güvenlik algoritmasını ve anahtarını belirtir.
        public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
        {
            // SigningCredentials nesnesi oluşturulurken, güvenlik anahtarı ve
            // kullanılacak imza algoritması parametre olarak verilir.
            // Burada HmacSha512Signature algoritması kullanılmaktadır, bu da HMAC-SHA512
            // imza algoritmasını temsil eder ve JWT token'larının imzalanması için güvenli bir seçenektir.
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
        }
    }

}
/*
Bu kod parçası, özellikle JSON Web Token (JWT) gibi token’ların imzalanması için kullanılır. SigningCredentials nesnesi, token’ı imzalamak için kullanılacak güvenlik anahtarını (securityKey) ve imza algoritmasını (SecurityAlgorithms.HmacSha512Signature) içerir.
HmacSha512Signature algoritması, token’ın bütünlüğünü ve doğruluğunu sağlamak için güçlü bir kriptografik imza sağlar. Bu nesne, token servis sağlayıcıları tarafından token’ları güvenli bir şekilde imzalamak için kullanılır. 
 
 
 
 */