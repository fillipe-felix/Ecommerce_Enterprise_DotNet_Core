using System.Threading;
using System.Threading.Tasks;
using EE.Cliente.API.Application.Events;
using EE.Cliente.API.Models;
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

        private readonly IClienteRepository _clienteRepository;

        /// <summary>
        /// Construtor ClienteCommandHandler
        /// </summary>
        /// <param name="clienteRepository"></param>
        public ClienteCommandHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<ValidationResult> Handle(RegistrarClienteCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValido())
            {
                return message.ValidationResult;
            }
            
            var cliente = new Models.Cliente(message.Id, message.Nome, message.Email, message.Cpf);
            
            //validações de negocio
            var clienteExistente = await _clienteRepository.ObterPorCpf(cliente.Cpf.Numero);
            
            //persistir no banco!
            if (clienteExistente != null) // ja existe o cliente com o CPF informado
            {
                AdicionarErro("Este CPF já está em uso.");
                return ValidationResult;
            }

            _clienteRepository.Adicionar(cliente);
            
            cliente.AdicionarEvento(new ClienteRegistradoEvent(message.Id, message.Nome, message.Email, message.Cpf));
            
            return await PersistirDados(_clienteRepository.UnitOfWork);
        }
    }
}