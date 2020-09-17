using DevBoost.Dronedelivery.Domain.Enumerators;
using DevBoost.DroneDelivery.Domain.Entities;
using DevBoost.DroneDelivery.Domain.Interfaces.Repositories;
using DevBoost.DroneDelivery.Infrastructure.Data.Contexts;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Infrastructure.Data.Repositories
{
    public class PedidoRepository : MongoRepository<Pedido>, IPedidoRepository
    {
        private readonly MongoDbContext<Pedido> _context;

        public PedidoRepository(MongoDbContext<Pedido> context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pedido>> ObterPedidosEmAberto()
        {

            return await _context.Collection.Find(e => e.Status == EnumStatusPedido.AguardandoEntregador).ToListAsync();

        }

        public async Task<IEnumerable<Pedido>> ObterPedidosEmTransito()
        {
            return await _context.Collection.Find(e => e.Status == EnumStatusPedido.EmTransito).ToListAsync();
        }


    }
}
