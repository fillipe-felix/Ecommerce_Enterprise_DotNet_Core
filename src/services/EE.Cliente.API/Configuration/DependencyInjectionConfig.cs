using EE.Cliente.API.Application.Commands;
using EE.Cliente.API.Application.Events;
using EE.Cliente.API.Data;
using EE.Cliente.API.Data.Repository;
using EE.Cliente.API.Models;
using EE.Core.Mediator;
using FluentValidation.Results;
using MediatR;
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
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IRequestHandler<RegistrarClienteCommand, ValidationResult>, ClienteCommandHandler>();

            services.AddScoped<INotificationHandler<ClienteRegistradoEvent>, ClienteEventHandler>();

            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<ClienteContext>();
        }
    }
}