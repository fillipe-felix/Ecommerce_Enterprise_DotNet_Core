using System;
using EE.Core.Messages;
using MediatR;

namespace EE.Cliente.API.Application.Events
{
    /// <summary>
    /// Class ClienteRegistradoEvent 
    /// </summary>
    public class ClienteRegistradoEvent : Event
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }
        
        /// <summary>
        /// Constructor ClienteRegistradoEvent
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nome"></param>
        /// <param name="email"></param>
        /// <param name="cpf"></param>
        public ClienteRegistradoEvent(Guid id, string nome, string email, string cpf)
        {
            AggregateId = id;
            Nome = nome;
            Email = email;
            Cpf = cpf;
        }
    }
}