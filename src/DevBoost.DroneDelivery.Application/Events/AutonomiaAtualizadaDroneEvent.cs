using DevBoost.DroneDelivery.Core.Domain.Messages;
using System.Diagnostics.CodeAnalysis;

namespace DevBoost.DroneDelivery.Application.Events
{
    [ExcludeFromCodeCoverage]
    public class AutonomiaAtualizadaDroneEvent : Event
    {

        public AutonomiaAtualizadaDroneEvent( int autonomiaRestante )
        {
            AutonomiaRestante = autonomiaRestante;
        }

        public int AutonomiaRestante { get; private set; }
    }
}
