using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using EE.WebApp.MVC.Extensions;
using EE.WebApp.MVC.Models;
using Microsoft.Extensions.Options;

namespace EE.WebApp.MVC.Services
{
    public class CatalogoService : Service, ICatalogoService
    {
        private readonly HttpClient _httpClient;

        public CatalogoService(HttpClient httpHttpClient, IOptions<AppSettings> settings)
        {
            httpHttpClient.BaseAddress = new Uri(settings.Value.CatalogoUrl);
            _httpClient = httpHttpClient;
        }

        public async Task<IEnumerable<ProdutoViewModel>> ObterTodos()
        {
            var response = await _httpClient.GetAsync("/catalogo/produtos");

            TratarErrosResponse(response);

            return await DesserializarObjetoResponse<IEnumerable<ProdutoViewModel>>(response);
        }

        public async Task<ProdutoViewModel> ObterPorId(Guid id)
        {
            var response = await _httpClient.GetAsync($"/catalogo/produtos/{id}");

            TratarErrosResponse(response);

            return await DesserializarObjetoResponse<ProdutoViewModel>(response);
        }
    }
}