using EE.Catalogo.API.Data;
using EE.Catalogo.API.Data.Repository;
using EE.Catalogo.API.Models;
using Microsoft.Extensions.DependencyInjection;

namespace EE.Catalogo.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<CatalogoContext>();
        }
    }
}