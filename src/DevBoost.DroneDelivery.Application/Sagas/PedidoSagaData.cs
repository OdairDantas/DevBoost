using Rebus.Sagas;

namespace DevBoost.DroneDelivery.Application.Sagas
{
    public class PedidoSagaData : SagaData
    {
        public bool PedidoRealizado { get; set; }
        public bool PagamentoRealizado { get; set; }
        public bool PedidoFinalizado { get; set; }

        public bool SagaCompleta => PedidoRealizado && PagamentoRealizado && PedidoFinalizado;
                                 
    }
}
