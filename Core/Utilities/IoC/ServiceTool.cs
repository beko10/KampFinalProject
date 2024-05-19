using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.IoC
{
    public static class ServiceTool
    {
        // Hizmet sağlayıcısını depolamak için bir özellik
        public static IServiceProvider ServiceProvider { get; private set; }

        // Hizmet koleksiyonunu oluşturan ve hizmet sağlayıcısını ayarlayan yöntem
        public static IServiceCollection Create(IServiceCollection services)
        {
            // Hizmet sağlayıcısını hizmet koleksiyonundan oluştur
            ServiceProvider = services.BuildServiceProvider();
            return services;
        }
    }

}
/*
 
Bu kod, .NET Core uygulamalarında bağımlılık enjeksiyonunu (DI) yönetmek için kullanılan bir yardımcı sınıfı temsil eder. 
ServiceTool sınıfı, hizmet koleksiyonunu oluşturur ve bu koleksiyonu kullanarak hizmet sağlayıcısını ayarlar. 
Hizmet sağlayıcısı, uygulama içindeki hizmetleri yönetmek ve çözmek için kullanılır.
 
 */