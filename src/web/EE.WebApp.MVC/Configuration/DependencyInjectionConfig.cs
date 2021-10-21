using System;
using EE.WebApp.MVC.Extensions;
using EE.WebApp.MVC.Services;
using EE.WebApp.MVC.Services.Handlers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EE.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Utiliza transient pq é chamado uma estancia de cada vez
            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

            services.AddHttpClient<IAutenticacaoService, AutenticacaoService>();

            services.AddHttpClient<ICatalogoService, CatalogoService>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

            // services.AddHttpClient("Refit", options =>
            //     {
            //         options.BaseAddress = new Uri(configuration.GetSection("CatalogoUrl").Value);
            //     })
            //     .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
            //     .AddTypedClient(Refit.RestService.For<ICatalogoServiceRefit>);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();
        }
    }
}