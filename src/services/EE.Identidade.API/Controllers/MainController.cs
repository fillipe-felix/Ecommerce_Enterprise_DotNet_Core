using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EE.Identidade.API.Controllers
{
    [ApiController]
    public abstract class MainController : Controller
    {
        protected ICollection<string> Errors = new List<string>();

        /// <summary>
        /// Response customizada para retorno de controllers personalizados
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected ActionResult CustomResponse(object result = null)
        {
            if (OperacaoValida())
            {
                return Ok(result);
            }

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "Mensagens", Errors.ToArray() }
            }));
        }

        /// <summary>
        /// Response customizada para retorno de controllers personalizados para ModelStates
        /// </summary>
        /// <param name="modelStateDictionary"></param>
        /// <returns></returns>
        protected ActionResult CustomResponse(ModelStateDictionary modelStateDictionary)
        {
            var erros = modelStateDictionary.Values.SelectMany(e => e.Errors);
            foreach (var erro in erros)
            {
                AdicionarErroProcessamento(erro.ErrorMessage);
            }

            return CustomResponse();
        } 

        /// <summary>
        /// Veifica se existe erros
        /// </summary>
        /// <returns></returns>
        protected bool OperacaoValida()
        {
            return !Errors.Any();
        }

        /// <summary>
        /// Adiciona erros dentro da collection Errors
        /// </summary>
        /// <param name="erro"></param>
        protected void AdicionarErroProcessamento(string erro)
        {
            Errors.Add(erro);
        }

        /// <summary>
        /// Limpa erros dentro da conllection Errors
        /// </summary>
        protected void LimparErrosProcessamento()
        {
            Errors.Clear();
        }
    }
}