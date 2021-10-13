﻿using EE.WebApp.MVC.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EE.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddHttpClient<IAutenticacaoService, AutenticacaoService>();
        }
    }
}