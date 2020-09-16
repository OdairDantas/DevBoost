using MediatR;
using System;

namespace DevBoost.DroneDelivery.Core.Domain.Messages
{
    public class Event : Message, INotification
    {
        public Event()
        {
            Timestamp = DateTime.Now;
            
        }

        public DateTime Timestamp { get; private set; }
        
    }
}
