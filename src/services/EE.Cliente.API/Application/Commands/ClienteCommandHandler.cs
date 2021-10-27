using System.Threading;
using System.Threading.Tasks;
using EE.Core.Messages;
using FluentValidation.Results;
using MediatR;

namespace EE.Cliente.API.Application.Commands
{
    /// <summary>
    /// Classe ClienteCommandHandler
    /// </summary>
    public class ClienteCommandHandler : CommandHandler, IRequestHandler<RegistrarClienteCommand, ValidationResult>
    {
        public async Task<ValidationResult> Handle(RegistrarClienteCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValido())
            {
                return message.ValidationResult;
            }
            
            var cliente = new Models.Cliente(message.Id, message.Nome, message.Email, message.Cpf);
            
            //validações de negocio
            
            //persistir no banco!
            if (true) // ja existe o cliente com o CPF informado
            {
                AdicionarErro("Este CPF já está em uso.");
                return ValidationResult;
            }
            
            return message.ValidationResult;
        }
    }
}