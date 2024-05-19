using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Hashing
{
    public class HashingHelper
    {
        //Kullanıcının kayıt olurken şifresi hashleyen metot 
        public static void CreatePasswordHash(string password,out byte[] passwordHash,out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                //passwordSalt değişkeni, hmac nesnesinin Key özelliği ile başlatılıyor. HMACSHA512 sınıfının Key özelliği, rastgele bir anahtar üretir ve bu anahtar,
                //şifre tuzunu (salt) oluşturmak için kullanılır.
                passwordSalt = hmac.Key;
                //Kullanıcının girdiği şifre ComputeHash fonksiyonu ile haslendi (ComputeHash fonksiyonu byte dizisi aldığından encoding edildi)
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));    
            }
        }
        //Kullanıcının giriş yaparken girdiği şifrenin veri tabanındaki şifresi aynı mı kontrol eden metot
        public static bool VerifyPasswordHash(string password,byte[] passwordHash,byte[] passwordSalt)
        {
            /*
             Metot Açıklaması 
             -----------------
             hmac nesnenesi kullanıcının uniqe key özelliğinden başlatıldı ve kullanıcının girdiği değer hashlendi ardından
             bu hashlenen değer ile db kayıtlı şifrenin hash değerleri karşılaştırıldı 
             
             */


            //hmac nesnesi passwordSalt ile başlatılıyor passwordSalt HMACSHA512 sınıfından Key propertsi verildi bu property kullanıcıya ait
            //uniqe bir key oluşturur
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                //Kullanıcının girdiği şifre hashlendi 
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                //Kullanıcının girdiği şifrenin hashi ile veri tabanındaki şifrenin hash değerleri karşılaştırıldı 
                for(int i = 0;i<computedHash.Length;i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                 

            }
            return true;
        }
    }
}
