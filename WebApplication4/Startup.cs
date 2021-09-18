using Core.Handlers;
using Core.Interfaces;
using Core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Npgsql;

namespace WebApplication4
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Alexander's Car Store", Version = "v1" });
            });

            services.AddSingleton<IVehicleProcessing, VehicleProcessing>();
            services.AddSingleton<IStatistic, StatisticHandler>();
            services.AddSingleton(new NpgsqlConnection(Configuration.GetConnectionString("PostgreSql")));        
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("https://localhost:5001/swagger/v1/swagger.json", "Alexander's Car Store v1"));
                app.UseSwaggerUI(c => c.SwaggerEndpoint("v1/swagger.json", "Alexander's Car Store v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            VehicleScheduler.Start(app.ApplicationServices.GetService<IVehicleProcessing>());
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
