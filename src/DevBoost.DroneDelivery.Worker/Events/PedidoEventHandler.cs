using DevBoost.DroneDelivery.Worker.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Worker.Events
{
    public class PedidoEventHandler : INotificationHandler<PedidoSolicitadoEvent>
    {
        private ILogger _logger;

        public async Task Handle(PedidoSolicitadoEvent message, CancellationToken cancellationToken)
        {
            try
            {
                var token = string.Empty;
                var credential = new { nome = "Mirosmar", senha = "Miro@miro.com" };
                using HttpClient login = new HttpClient();
                {

                    var response = await login.PostAsync("http://localhost:50648/login", JsonConvert.SerializeObject(credential).ConvertObjectToByteArrayContent());
                    if (!response.IsSuccessStatusCode) return;

                    token = await response.Content.ReadAsStringAsync();
                }

                using HttpClient client = new HttpClient();
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                    await client.PostAsync("http://localhost:50648/api/pedido", JsonConvert.SerializeObject(message).ConvertObjectToByteArrayContent());
                }

            }
            catch (System.Exception)
            {

                _logger.LogCritical($"algo errado aconteceu");
            }


        }
    }
}
