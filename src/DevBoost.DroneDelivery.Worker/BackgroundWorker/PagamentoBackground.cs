using AutoMapper;
using DevBoost.DroneDelivery.Core.Domain.Interfaces.Handlers;
using DevBoost.DroneDelivery.Worker.Events;
using KafkaNet;
using KafkaNet.Model;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Worker.BackgroundWorker
{
    public class PagamentoBackground : IHostedService
    {
        private IMediatrHandler _mediatr;
        private IMapper _mapper;
        private readonly ILogger<PagamentoBackground> _logger;
        private KafkaOptions _kafkaOptions;
        private BrokerRouter _brokerRouter;
        private Consumer _consumer;

        public PagamentoBackground(ILogger<PagamentoBackground> logger, IMapper mapper,IMediatrHandler mediatr)
        {
            _mediatr = mediatr;
            _mapper = mapper;
            _kafkaOptions = new KafkaOptions(new Uri("http://localhost:9092"));
            _brokerRouter = new BrokerRouter(_kafkaOptions);
            _consumer = new Consumer(new ConsumerOptions("pedido-response", _brokerRouter));
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
                    
                    var obj = JsonConvert.DeserializeObject<PedidoSolicitadoEvent>(Encoding.UTF8.GetString(msg.Value));
                    var evento = _mapper.Map<PagamentoSolicitadoEvent>(obj);
                    await _mediatr.PublicarEvento(evento);
                }
                catch (Exception ex)
                {
                    _logger.LogCritical($"algo errado aconteceu");
                }
                

            }
        }
    }
}
