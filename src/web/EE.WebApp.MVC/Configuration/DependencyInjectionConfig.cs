using System;
using System.Net.Http;
using EE.WebApp.MVC.Extensions;
using EE.WebApp.MVC.Services;
using EE.WebApp.MVC.Services.Handlers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;

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
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                // .AddTransientHttpErrorPolicy(
                //     p => p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(600)));
                .AddPolicyHandler(PollyExtensions.EsperarTentar())
                .AddTransientHttpErrorPolicy(
                    p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            #region Refit
            // services.AddHttpClient("Refit", options =>
            //     {
            //         options.BaseAddress = new Uri(configuration.GetSection("CatalogoUrl").Value);
            //     })
            //     .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
            //     .AddTypedClient(Refit.RestService.For<ICatalogoServiceRefit>);
            #endregion
            
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();
        }
    }

    public static class PollyExtensions
    {
        public static AsyncRetryPolicy<HttpResponseMessage> EsperarTentar()
        {
            var retryWaitPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(new[]
                {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(10),
                }, (outcome, timeSpan, retryCount, context) =>
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"Tentando pela {retryCount} vez!");
                    Console.ForegroundColor = ConsoleColor.White;
                });
            return retryWaitPolicy;
        }
    }
}