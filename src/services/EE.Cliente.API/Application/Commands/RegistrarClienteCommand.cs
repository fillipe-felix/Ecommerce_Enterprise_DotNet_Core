using System;
using EE.Core.Messages;

namespace EE.Cliente.API.Application.Commands
{
    /// <summary>
    /// Classe RegistrarClienteCommand
    /// </summary>
    public class RegistrarClienteCommand : Command
    {
        /// <summary>
        /// Id da classe
        /// </summary>
        public Guid Id { get; private set; }
        
        /// <summary>
        /// Nome do cliente
        /// </summary>
        public string Nome { get; private set; }

        /// <summary>
        /// Email do cliente
        /// </summary>
        public string Email { get; private set; }
        
        /// <summary>
        /// Cpf do cliente
        /// </summary>
        public string Cpf { get; private set; }

        /// <summary>
        /// Construtor RegistrarClienteCommand
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nome"></param>
        /// <param name="email"></param>
        /// <param name="cpf"></param>
        public RegistrarClienteCommand(Guid id, string nome, string email, string cpf)
        {
            AggregateId = id;
            Id = id;
            Nome = nome;
            Email = email;
            Cpf = cpf;
        }
    }
}