using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Infrastructure.Data.Contexts
{
    public class PedidoMongoContext: IPedidoMongoContext 
    {
        public string PedidosCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }

        public Task<bool> Commit()
        {
            throw new System.NotImplementedException();
        }
    }
}
