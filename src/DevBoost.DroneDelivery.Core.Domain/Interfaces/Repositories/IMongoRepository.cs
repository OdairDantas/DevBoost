using DevBoost.DroneDelivery.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Core.Domain.Interfaces.Repositories
{
    public interface IMongoRepository<T> : IDisposable where T :Entity
    {
        Task<IEnumerable<T>> ObterTodos();
        Task<T> ObterPorId(Guid id);
        Task<T> ObterPorId(int id);
        Task Adicionar(T entity);
        Task Atualizar(T entity);
        Task<IEnumerable<T>> ObterPor(Expression<Func<T, bool>> predicate);
    }
}
