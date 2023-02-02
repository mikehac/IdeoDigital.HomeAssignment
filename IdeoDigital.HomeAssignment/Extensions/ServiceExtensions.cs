using IdeoDigital.Entities;
using Microsoft.EntityFrameworkCore;

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
    }
}
