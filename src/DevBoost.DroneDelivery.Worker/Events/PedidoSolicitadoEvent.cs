using DevBoost.DroneDelivery.Core.Domain.Messages;

namespace DevBoost.DroneDelivery.Worker.Events
{
    public class PedidoSolicitadoEvent : Event
    {
        public string usuario { get; set; }
        public int Peso { get; set; }
        public string NumeroCartao { get; set; }
        public string Bandeira { get; set; }
        public int MesVencimento { get; set; }
        public int AnoVencimento { get; set; }
        public double Valor { get; set; }
    }
}
