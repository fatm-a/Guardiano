using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carbook.Application.Services
{
    public static class ServiceRegistiration
    {
        public static void  AddApplicationService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg=>cfg.RegisterServicesFromAssembly(typeof(ServiceRegistiration).Assembly));
        }
    }
}
//metod, services koleksiyonuna MediatR kütüphanesini ekledik..
//cfg.RegisterServicesFromAssembly ile,
//ServiceRegistiration sınıfının bulunduğu assembly içindeki MediatR handler'larını otomatik olarak bulup kayıt ediyor.
//ve Projendeki MediatR komutları, sorguları ve handler’ları dependency injection’a ekleyerek, uygulama genelinde kolayca kullanabilmeyi sağladık.
//Bu metodu Startup veya Program.cs içinde çağırarak MediatR servisini aktif ettik.