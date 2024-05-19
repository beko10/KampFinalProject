using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
//Extensions bir sınıfı genişletmektir
//NOT: Extensions metot yazabilmek için sınıfın ve metodun static olması lazım.
namespace Core.Extensions
{
    public static class ClaimExtensions
    {
        // 'AddEmail' yöntemi, bir 'Claim' koleksiyonuna yeni bir e-posta iddiası ekler.
        public static void AddEmail(this ICollection<Claim> claims, string email)
        {
            // 'JwtRegisteredClaimNames.Email' kullanarak yeni bir 'Claim' oluşturur ve koleksiyona ekler.
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, email));
        }

        // 'AddName' yöntemi, bir 'Claim' koleksiyonuna yeni bir isim iddiası ekler.
        public static void AddName(this ICollection<Claim> claims, string name)
        {
            // 'ClaimTypes.Name' kullanarak yeni bir 'Claim' oluşturur ve koleksiyona ekler.
            claims.Add(new Claim(ClaimTypes.Name, name));
        }

        // 'AddNameIdentifier' yöntemi, bir 'Claim' koleksiyonuna yeni bir isim tanımlayıcı iddiası ekler.
        public static void AddNameIdentifier(this ICollection<Claim> claims, string nameIdentifier)
        {
            // 'ClaimTypes.NameIdentifier' kullanarak yeni bir 'Claim' oluşturur ve koleksiyona ekler.
            claims.Add(new Claim(ClaimTypes.NameIdentifier, nameIdentifier));
        }

        // 'AddRoles' yöntemi, bir 'Claim' koleksiyonuna yeni rol iddiaları ekler.
        public static void AddRoles(this ICollection<Claim> claims, string[] roles)
        {
            // 'roles' dizisindeki her bir rol için bir 'Claim' oluşturur ve koleksiyona ekler.
            // 'ForEach' metodu ile 'roles' listesindeki her bir eleman için belirtilen işlemi uygular.
            roles.ToList().ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));
        }
    }

}
