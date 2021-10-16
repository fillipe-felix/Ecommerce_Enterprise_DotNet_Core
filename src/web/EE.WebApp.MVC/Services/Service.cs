using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EE.WebApp.MVC.Extensions;

namespace EE.WebApp.MVC.Services
{
    public abstract class Service
    {

        protected StringContent ObterConteudo(object obj)
        {
            return new StringContent(
                JsonSerializer.Serialize(obj),
                Encoding.UTF8,
                "application/json");
        }

        protected async Task<T> DesserializarObjetoResponse<T>(HttpResponseMessage responseMessage)
        {
            //usado para que ignore o case sensitive dos atributos das classes e consiga desseralizar o objeto corretamente
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };
            
            return JsonSerializer.Deserialize<T>(await responseMessage.Content.ReadAsStringAsync(), options);
        }
        
        protected bool TratarErrosResponse(HttpResponseMessage response)
        {
            switch ((int)response.StatusCode)
            {
                case 401:
                case 403:
                case 404:
                case 500:
                    throw new CustomHttpResquestException(response.StatusCode);
                case 400:
                    return false;
            }

            //garante que o codigo retornado seja de sucesso
            response.EnsureSuccessStatusCode();
            return true;
        }
        
        
    }
}