using System.Collections.Generic;
using System.Threading.Tasks;
using EE.Cliente.API.Models;
using EE.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace EE.Cliente.API.Data.Repository
{
    /// <summary>
    /// Classe ClienteRepository
    /// </summary>
    public class ClienteRepository : IClienteRepository
    {
        private readonly ClienteContext _clienteContext;
        public IUnitOfWork UnitOfWork => _clienteContext;

        /// <summary>
        /// Construtor ClienteRepository
        /// </summary>
        /// <param name="clienteContext"></param>
        public ClienteRepository(ClienteContext clienteContext)
        {
            _clienteContext = clienteContext;
        }

        /// <summary>
        /// Adiciona um cliente no banco de dados 
        /// </summary>
        /// <param name="cliente"></param>
        public void Adicionar(Models.Cliente cliente)
        {
            _clienteContext.Clientes.Add(cliente);
        }

        /// <summary>
        /// Retorna todos os clientes da base de dados
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Models.Cliente>> ObterTodos()
        {
            return await _clienteContext.Clientes.AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// Retorna o cliente com base no CPF
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        public async Task<Models.Cliente> ObterPorCpf(string cpf)
        {
            return await _clienteContext.Clientes.FirstOrDefaultAsync(c => c.Cpf.Numero == cpf);
        }
        
        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            _clienteContext.Dispose();
        }
    }
}