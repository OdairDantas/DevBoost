using DevBoost.DroneDelivery.Application.Commands;
using DevBoost.DroneDelivery.Application.Events;
using DevBoost.DroneDelivery.Core.Domain.Messages.IntegrationEvents;
using Rebus.Bus;
using Rebus.Handlers;
using Rebus.Sagas;
using System;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Application.Sagas
{
    public class PedidoSaga : Saga<PedidoSagaData>, IAmInitiatedBy<PedidoAdicionadoEvent>, IHandleMessages<PagamentoCartaoProcessadoEvent>, IHandleMessages<PedidoDespachadoEvent>
    {
        private readonly IBus _bus;

        public PedidoSaga(IBus bus)
        {
            _bus = bus;
        }

        public async Task Handle(PedidoAdicionadoEvent message)
        {
            Data.PedidoRealizado = true;
            ProcessoSaga();
        }

        public async Task Handle(PagamentoCartaoProcessadoEvent message)
        {
            _bus.Publish(new PagamentoCartaoProcessadoEvent(message.PedidoId, message.SituacaoPagamento) { AggregateRoot = message.AggregateRoot}).Wait();

            Data.PagamentoRealizado = true;
            ProcessoSaga();
        }

        public async Task Handle(PedidoDespachadoEvent message)
        {
            Data.PedidoFinalizado = true;
            ProcessoSaga();
            
        }

        protected override void CorrelateMessages(ICorrelationConfig<PedidoSagaData> config)
        {
            config.Correlate<AdicionarPedidoCommand>(m => m.AggregateRoot, d => d.Id);
            config.Correlate<PedidoAdicionadoEvent>(m => m.AggregateRoot, d => d.Id);
            config.Correlate<PagamentoCartaoProcessadoEvent>(m => m.PedidoId, d => d.Id);
            config.Correlate<PedidoDespachadoEvent>(m => m.AggregateRoot, d => d.Id);
            config.Correlate<PedidoDespachadoEvent>(m => m.AggregateRoot, d => d.Id);
        }

        public void ProcessoSaga()
        {
            if (Data.SagaCompleta) MarkAsComplete();

        }
    }
}
