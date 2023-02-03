using IdeoDigital.Contracts;
using IdeoDigital.Entities;
using IdeoDigital.HomeAssignment.Services;
using IdeoDigital.Repository;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace IdeoDigital.HomeAssignment.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config["ConnectionStrings:IdeoDigitalDb"];

            services.AddDbContext<IdeoDigitalContext>(o =>
            o.UseSqlServer(connectionString, b => b.MigrationsAssembly("IdeoDigital.Entities")));
        }

        public static void AddDIs(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly()); 
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            services.AddScoped<IInvoiceService, InvoiceService>();
        }
    }
}
