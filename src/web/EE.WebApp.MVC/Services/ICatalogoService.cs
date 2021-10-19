using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EE.WebApp.MVC.Models;

namespace EE.WebApp.MVC.Services
{
    public interface ICatalogoService
    {
        Task<IEnumerable<ProdutoViewModel>> ObterTodos();
        Task<ProdutoViewModel> ObterPorId(Guid id);
    }
}