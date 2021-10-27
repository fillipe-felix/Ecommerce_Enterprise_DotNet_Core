using System.Threading.Tasks;
using EE.Core.Messages;
using FluentValidation.Results;

namespace EE.Core.Mediator
{
    /// <summary>
    /// Interface IMediatorHandler
    /// </summary>
    public interface IMediatorHandler
    {
        /// <summary>
        /// Faz a publicação de um Evento onde a classe precise herdar de Event
        /// </summary>
        /// <param name="evento"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task PublicarEvento<T>(T evento) where T : Event;
        
        /// <summary>
        /// Envia um comando onde a classe obrigatóriamente precisa herdar de Command
        /// </summary>
        /// <param name="comando"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<ValidationResult> EnviarComando<T>(T comando) where T : Command;
    }
}