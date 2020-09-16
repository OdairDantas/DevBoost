using DevBoost.DroneDelivery.Worker.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Worker.Events
{
    public class PagamentoEventHandler : INotificationHandler<PagamentoSolicitadoEvent>
    {
        private ILogger _logger;
        public PagamentoEventHandler(ILogger<PagamentoEventHandler> logger)
        {
            _logger = logger;
        }
        public async Task Handle(PagamentoSolicitadoEvent message, CancellationToken cancellationToken)
        {
            try
            {

                using (HttpClient client = new HttpClient())
                    await client.PostAsync("http://localhost:44393/api/pagamento", JsonConvert.SerializeObject(message).ConvertObjectToByteArrayContent());
            }
            catch (System.Exception)
            {
                _logger.LogCritical($"algo errado aconteceu");

            }
        }
    }
}
