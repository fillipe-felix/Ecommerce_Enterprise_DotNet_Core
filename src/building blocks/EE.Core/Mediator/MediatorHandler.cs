using System.Threading.Tasks;
using EE.Core.Messages;
using FluentValidation.Results;
using MediatR;

namespace EE.Core.Mediator
{
    /// <summary>
    /// Classe de implementação do MediatorHandler
    /// </summary>
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        
        /// <summary> 
        /// Construtor MediatorHandler
        /// </summary>
        /// <param name="mediator"></param>
        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Faz a publicação de um Evento onde a classe precise herdar de Event
        /// </summary>
        /// <param name="evento"></param>
        /// <typeparam name="T"></typeparam>
        public async Task PublicarEvento<T>(T evento) where T : Event
        {
            await _mediator.Publish(evento);
        }

        /// <summary>
        /// Envia um comando onde a classe obrigatóriamente precisa herdar de Command
        /// </summary>
        /// <param name="comando"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<ValidationResult> EnviarComando<T>(T comando) where T : Command
        {
            return await _mediator.Send(comando);
        }
    }
}