using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EE.WebApp.MVC.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EE.WebApp.MVC
{
    public class Startup
    {
        public Startup(IHostEnvironment hostEnvironment)
        {
            //faz a configuração de diferentes ambientes de desenvolvimento
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

            if (hostEnvironment.IsDevelopment()) builder.AddUserSecrets<Startup>();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //adiciona as configurações do Identity
            services.AddIdentityConfugation();

            services.AddMvcConfiguration(Configuration);

            // adiciona configurações de injeção de dependencia de services
            services.RegisterServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMvcConfiguraion(env);
        }
    }
}