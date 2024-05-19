using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Configuration propertysi ile appsettings.json dosyasından okuduğumuz TokenOptions alanına karşılık gelen sınıf
namespace Core.Utilities.Security.JWT
{
    public class TokenOptions
    {
        public string? Audience { get; set; }
        public string? Issure { get; set; }
        public int AccesTokenExpiration { get; set; }
        public string? SecurityKey { get; set; }
    }

}
