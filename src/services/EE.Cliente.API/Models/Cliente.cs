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
        /// <param name="nome"></param>
        /// <param name="email"></param>
        /// <param name="cpf"></param>
        /// <param name="excluido"></param>
        public Cliente(string nome, string email, string cpf, bool excluido)
        {
            Nome = nome;
            Email = email;
            Cpf = cpf;
            Excluido = false;
        }
        
        /// <summary>
        /// EF Relacionamento
        /// </summary>
        protected Cliente(){ }

        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }
        public bool Excluido { get; private set; }
        public Endereco Endereco { get; private set; }
    }
}