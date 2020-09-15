using DevBoost.DroneDelivery.Worker.Extensions;
using KafkaNet;
using KafkaNet.Model;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace DevBoost.DroneDelivery.Worker.BackgroundWorker
{
    public class PedidoBackground : IHostedService
    {
        private readonly ILogger<PedidoBackground> _logger;
        private KafkaOptions _kafkaOptions;
        private BrokerRouter _brokerRouter;
        private Consumer _consumer;

        public PedidoBackground(ILogger<PedidoBackground> logger)
        {
            _kafkaOptions = new KafkaOptions(new Uri("http://localhost:9092"));
            _brokerRouter = new BrokerRouter(_kafkaOptions);
            _consumer = new Consumer(new ConsumerOptions("pedido-request", _brokerRouter));
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken) => await Task.Run(() => new Timer(ExecuteProcess, null, TimeSpan.Zero, TimeSpan.FromSeconds(5)), cancellationToken);


        public async Task StopAsync(CancellationToken cancellationToken) => await Task.Run(() => _logger.LogInformation("Serviço parado!"), cancellationToken);


        protected void ExecuteProcess(object state) => ObterAsync().Wait();



        private async Task ObterAsync()
        {
            foreach (var msg in _consumer.Consume())
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
                        await client.PostAsync("http://localhost:50648/api/pedido", Encoding.UTF8.GetString(msg.Value).ConvertObjectToByteArrayContent());
                    }
                }
                catch (Exception ex)
                {

                    _logger.LogCritical($"algo errado aconteceu : {ex}");
                }


            }
        }
    }
}
