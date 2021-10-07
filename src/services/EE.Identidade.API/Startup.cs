using EE.Identidade.API.Configuration;
using EE.Identidade.API.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EE.Identidade.API
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
            //Realiza conexão com o sql server
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            //Configurações do Identity
            services.AddIdentityConfiguration(Configuration);

            //Configurações da API
            services.AddApiConfiguration();

            //Adicionas as configurações do swagger
            services.AddSwaggerConfiguration();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //usa as configurações do swagger
            app.UseSwaggerConfiguration();

            //usa as configurações da API
            app.UseApiConfiguration(env);
        }
    }
}