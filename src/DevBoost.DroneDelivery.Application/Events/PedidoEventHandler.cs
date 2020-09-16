using DevBoost.DroneDelivery.Core.Domain.Messages.IntegrationEvents;
using KafkaNet;
using KafkaNet.Model;
using MediatR;
using Newtonsoft.Json;
using Rebus.Bus;
using Rebus.Handlers;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Application.Events
{
    public class PedidoEventHandler : INotificationHandler<PedidoAdicionadoEvent>, IHandleMessages<PedidoAdicionadoEvent>
    {
        private readonly IBus _bus;
        public PedidoEventHandler(IBus bus)
        {
            _bus = bus;
        }
        public PedidoEventHandler()
        {

        }
        public async Task Handle(PedidoAdicionadoEvent message, CancellationToken cancellationToken)
        {

           

            Uri uri = new Uri("http://localhost:9092");

            const string topicName = "pedido-response";

            var obj = new { PedidoId = message.AggregateRoot, message.BandeiraCartao, message.AnoVencimentoCartao, message.MesVencimentoCartao, message.NumeroCartao, message.Valor };

            await Task.Run(() =>
            {
                KafkaNet.Protocol.Message msg = new KafkaNet.Protocol.Message(JsonConvert.SerializeObject(obj));
                var options = new KafkaOptions(uri);
                using var router = new BrokerRouter(options);
                using var client = new Producer(router);
                client.SendMessageAsync(topicName, new List<KafkaNet.Protocol.Message> { msg }).Wait();
            });
            






        }

        public async Task Handle(PedidoAdicionadoEvent message)
        {
            _bus.Publish(message).Wait();
        }
    }
}
