using System.Collections.Generic;
using System.Threading.Tasks;
using EE.Core.Data;

namespace EE.Cliente.API.Models
{
    /// <summary>
    /// Interface IClienteRepository
    /// </summary>
    public interface IClienteRepository : IRepository<Cliente>
    {
        /// <summary>
        /// Adiciona um cliente no banco
        /// </summary>
        /// <param name="cliente"></param>
        void Adicionar(Cliente cliente);

        /// <summary>
        /// Retorna todos os clientes
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Cliente>> ObterTodos();
        
        /// <summary>
        /// Obtem um cliente com base no CPF
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        Task<Cliente> ObterPorCpf(string cpf);
    }
}