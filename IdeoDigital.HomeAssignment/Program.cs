
using IdeoDigital.Contracts;
using IdeoDigital.HomeAssignment.Extensions;
using IdeoDigital.Repository;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using System.Text.Json.Serialization;

namespace IdeoDigital.HomeAssignment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.ConfigureSqlContext(builder.Configuration);
            builder.Services.AddDIs();
            //builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
            builder.Services.AddControllers()
                .AddJsonOptions(x => 
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());


            app.Run();
        }
    }
}