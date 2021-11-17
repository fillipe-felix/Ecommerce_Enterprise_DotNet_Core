using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace EE.Cliente.API.Application.Events
{
    /// <summary>
    /// Class ClienteEventHandler
    /// </summary>
    public class ClienteEventHandler : INotificationHandler<ClienteRegistradoEvent>
    {
        public Task Handle(ClienteRegistradoEvent notification, CancellationToken cancellationToken)
        {
            // Enviar evento de confirmação
            return Task.CompletedTask;
        }
    }
}