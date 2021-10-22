using System;
using EE.Core.DomainObjects;

namespace EE.Cliente.API.Models
{
    /// <summary>
    /// Classe Endereço
    /// </summary>
    public class Endereco : Entity
    {
        
        /// <summary>
        /// Construtor Endereco
        /// </summary>
        /// <param name="logradouro"></param>
        /// <param name="numero"></param>
        /// <param name="complemento"></param>
        /// <param name="bairro"></param>
        /// <param name="cep"></param>
        /// <param name="cidade"></param>
        /// <param name="estado"></param>
        public Endereco(string logradouro, string numero, string complemento, string bairro, string cep, string cidade, string estado)
        {
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            Cep = cep;
            Cidade = cidade;
            Estado = estado;
        }
        
        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Bairro { get; private set; }
        public string Cep { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }
        public Guid ClienteId { get; private set; }
        // EF Relacionamento
        // Relacionamento de 1:1
        public Cliente Cliente { get; set; }
    }
}