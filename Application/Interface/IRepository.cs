using System.Linq.Expressions;

namespace Application.Interface;

public interface IRepository
{
}

public interface IRepository<T> : IRepository
{
    T Add(T entity);
    Task<T> GetByKey(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate,CancellationToken cancellationToken);
    Task<T> FindSingle(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    int AddRange(List<T> entities);
}