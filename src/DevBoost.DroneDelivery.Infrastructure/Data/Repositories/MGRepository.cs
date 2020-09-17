using DevBoost.DroneDelivery.Core.Domain.Interfaces.Repositories;
using DevBoost.DroneDelivery.Domain.Entities;
using DevBoost.DroneDelivery.Domain.Interfaces.Repositories;
using DevBoost.DroneDelivery.Infrastructure.Data.Contexts;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Infrastructure.Data.Repositories
{
    public class MGRepository : IMGRepository
    {
        private readonly IPedidoMongoContext _context;
        private readonly IMongoCollection<Pedido> _repo;

        public MGRepository(IPedidoMongoContext context)
        {

            _context = context;
            var client = new MongoClient(context.ConnectionString);
            var database = client.GetDatabase(context.DatabaseName);
            _repo = database.GetCollection<Pedido>(context.PedidosCollectionName);
        }


        public IUnitOfWork UnitOfWork => throw new NotImplementedException();

        public async Task Adicionar(Pedido entity)
        {
            _repo.InsertOne(entity);

        }

        public async Task Atualizar(Pedido entity)
        {
            _repo.ReplaceOne(e => e.Id == entity.Id, entity);
        }

        public async Task<IEnumerable<Pedido>> ObterPor(Expression<Func<Pedido, bool>> predicate)
        {
            return _repo.Find(filter: predicate).ToList();
        }

        public async Task<Pedido> ObterPorId(Guid id)
        {
            


            return await _repo.Find(c => c.Id == id).FirstOrDefaultAsync();
            //return _repo.Find(e => true).ToList().FirstOrDefault(t => t.Id == id);

        }

        public Task<Pedido> ObterPorId(int id)
        {
            throw new NotImplementedException();

        }

        public async Task<IEnumerable<Pedido>> ObterTodos()
        {
            return _repo.Find(e => true).ToList();

        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }




}
