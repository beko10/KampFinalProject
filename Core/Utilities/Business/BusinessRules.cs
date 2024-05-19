using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Business
{
    public class BusinessRules
    {
        // 'BusinessRules' adında bir sınıf tanımlanmış. Bu sınıf içinde iş kurallarını kontrol eden metotlar olabilir.

        public static IResult Run(params IResult[] logics)
        {
            // 'Run' adında bir statik metot tanımlanmış. Bu metot, 'IResult' tipinde bir dizi alıyor.
            // 'params' anahtar kelimesi sayesinde, bu metoda istenilen sayıda 'IResult' nesnesi gönderilebilir.

            foreach (var logic in logics)
            {
                // 'logics' dizisindeki her bir 'logic' nesnesi için aşağıdaki kod bloğu çalıştırılacak.
                if (!logic.Success)
                {
                    // Eğer 'logic' nesnesinin 'Success' özelliği 'false' ise,
                    // yani iş kuralı başarısızsa, o zaman bu 'logic' nesnesi geri döndürülecek.

                    return logic;
                }
            }
            // Eğer tüm 'logic' nesnelerinin 'Success' özelliği 'true' ise,
            // yani tüm iş kuralları başarılıysa, o zaman 'null' geri döndürülecek.

            return null;
        }
    }

}
