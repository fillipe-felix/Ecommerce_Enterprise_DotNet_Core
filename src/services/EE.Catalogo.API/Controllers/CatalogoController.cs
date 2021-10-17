using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EE.Catalogo.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace EE.Catalogo.API.Controllers
{
    [ApiController]
    public class CatalogoController : Controller
    {
        private readonly IProdutoRepository _produtoRepository;

        public CatalogoController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        /// <summary>
        /// Retorna a lista com todos os produtos
        /// </summary>
        /// <returns>Produto</returns>
        [HttpGet("catalogo/produtos")]
        public async Task<IEnumerable<Produto>> Index()
        {
            return await _produtoRepository.ObterTodos();
        }

        /// <summary>
        /// Retorna detalhes de um produto
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Produto</returns>
        [HttpGet("catalogo/produtos/{id}")]
        public async Task<Produto> ProdutoDetalhe(Guid id)
        {
            return await _produtoRepository.ObterPorId(id);
        }
    }
}