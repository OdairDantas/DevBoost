using DevBoost.DroneDelivery.Core.Domain.Interfaces.Repositories;

namespace DevBoost.DroneDelivery.Infrastructure.Data.Contexts
{
    public interface IPedidoMongoContext
    {
        string PedidosCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        
    }
}
