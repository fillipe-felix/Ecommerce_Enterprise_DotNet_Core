using EE.Cliente.API.Data;
using Microsoft.Extensions.DependencyInjection;

namespace EE.Cliente.API.Configuration
{
    /// <summary>
    /// Classe de injeção de dependência 
    /// </summary>
    public static class DependencyInjectionConfig
    {
        /// <summary>
        /// Método de injeção de dependência
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ClienteContext>();
        }
    }
}