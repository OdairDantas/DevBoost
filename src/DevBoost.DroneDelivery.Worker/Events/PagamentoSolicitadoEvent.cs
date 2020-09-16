using DevBoost.DroneDelivery.Core.Domain.Messages;
using System;

namespace DevBoost.DroneDelivery.Worker.Events
{
    public class PagamentoSolicitadoEvent: Event
    {
        public Guid PedidoId { get;  set; }
        public double Valor { get;  set; }
        public string BandeiraCartao { get;  set; }
        public string NumeroCartao { get;  set; }
        public int MesVencimentoCartao { get;  set; }
        public int AnoVencimentoCartao { get; set; }
    }
}
