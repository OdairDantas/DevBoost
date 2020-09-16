namespace DevBoost.DroneDelivery.Core.Domain.Messages.IntegrationEvents
{

    public class PedidoSolicitadoEvent
    {
        public string Usuario { get; set; }
        public int Peso { get; set; }
        public string NumeroCartao { get; set; }
        public string Bandeira { get; set; }
        public short MesVencimento { get; set; }
        public short AnoVencimento { get; set; }
        public double Valor { get; set; }
    }
}
