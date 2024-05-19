using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilities.Security.Encryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace Core.Utilities.Security.JWT
{
    public class JwtHelper : ITokenHelper
    {
        //IConfiguration arayüzü API'deki appsettings.json dosyasını okumaya yarayan bir arayüzdür
        public IConfiguration Configuration { get; } // appsettings.json dosyasını okur

        // appsettings.json da dosyasında  bulunan TokenOptions bilgileri bilgilerini tutacak TokenOptions tipindeki field
        private TokenOptions _tokenOptions;
        
        //
        private DateTime _accessTokenExpiration;

        public JwtHelper(IConfiguration configuration)
        {
            //Configuration enjekte edildi 
            Configuration = configuration;
            //appsettings.json dosyasından TokenOptions kısmı Configuration propertysi üzerinden okundu ve Get<TokenOptions> ile okunan section değerleri TokenOptions sınıfı ile ilgili yerler ilişkilendirildi
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
        }

        public AccessToken CreateToken(User user, List<OperationClaim> operationClaims)
        {
            // Token'ın bitiş süresini hesapla
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccesTokenExpiration);
            
            // Güvenlik anahtarını oluştur(SecurityKeyHelper sınıfındaki CreateSecurityKey metoduna parametre olarak appsettings.json dosyasındaki SecurityKey gönderilerek güvenlik anahtarı oluşturuldu )
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);

            // İmza bilgilerini oluştur(SigningCredentialsHelper sınıfının CreateSigningCredentials metoduna bir üst satırda oluşturulan securityKey parametre olarak gönderilerek oluşturuldu)
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);

            // JWT güvenlik belgesini oluştur
            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, operationClaims);

            // JWT güvenlik belgesini işleyici ile yazarak token oluştur
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };
        }

        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user,
            SigningCredentials signingCredentials, List<OperationClaim> operationClaims)
        {
            // JWT oluştur
            var jwt = new JwtSecurityToken(
                //jwt ilgili alanları dolduruldu 
                issuer: tokenOptions.Issure,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(user, operationClaims),
                signingCredentials: signingCredentials
            );
            return jwt;
        }

        private IEnumerable<Claim> SetClaims(User user, List<OperationClaim> operationClaims)
        {
            var claims = new List<Claim>();

            // Kullanıcıya ait kimlik bilgilerini ekle
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddEmail(user.Email);// claims.Add(new Claim("email",user.Email))
            claims.AddName($"{user.FirstName} {user.LastName}");

            // Kullanıcının rollerini ekleyin
            claims.AddRoles(operationClaims.Select(c => c.Name).ToArray());

            return claims;
        }
    }

}
