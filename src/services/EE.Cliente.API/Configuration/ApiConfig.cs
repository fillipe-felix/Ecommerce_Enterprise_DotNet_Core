using EE.Cliente.API.Data;
using EE.WebApi.Core.Identidade;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EE.Cliente.API.Configuration
{
    /// <summary>
    /// Classe de configurações de API
    /// </summary>
    public static class ApiConfig
    {
        /// <summary>
        /// Método para adicionar configurações de API
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ClienteContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddControllers();

            services.AddCors(options =>
            {
                //Permite acesso total a origens, metodos e headers
                options.AddPolicy("Total",
                    builder =>
                        builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader());
            });
        }

        /// <summary>
        /// Método para usar as configurações de API
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public static void UseApiConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            
            app.UseRouting();

            app.UseCors("Total");

            app.UseAuthConfiguration();

            app.UseEndpoints(endPoints =>
            {
                endPoints.MapControllers();
            });
        }
    }
}