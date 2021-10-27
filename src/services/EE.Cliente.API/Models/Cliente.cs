using System;
using EE.Core.DomainObjects;

namespace EE.Cliente.API.Models
{
    /// <summary>
    /// Classe Cliente
    /// </summary>
    public class Cliente : Entity, IAggregateRoot
    {
        /// <summary>
        /// Construtor Cliente
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nome"></param>
        /// <param name="email"></param>
        /// <param name="cpf"></param>
        /// <param name="excluido"></param>
        public Cliente(Guid id, string nome, string email, string cpf)
        {
            Id = id;
            Nome = nome;
            Email = new Email(email);
            Cpf = new Cpf(cpf);
            Excluido = false;
        }
        
        /// <summary>
        /// EF Relacionamento
        /// </summary>
        protected Cliente(){ }

        public string Nome { get; private set; }
        public Email Email { get; private set; }
        public Cpf Cpf { get; private set; }
        public bool Excluido { get; private set; }
        public Endereco Endereco { get; private set; }

        public void TrocarEmail(string email)
        {
            Email = new Email(email);
        }

        public void AtribuirEndereco(Endereco endereco)
        {
            Endereco = endereco;
        }
    }
}