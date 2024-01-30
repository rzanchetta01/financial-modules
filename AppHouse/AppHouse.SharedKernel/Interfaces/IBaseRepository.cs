using AppHouse.SharedKernel.BaseClasses;

namespace AppHouse.SharedKernel.Interfaces
{
    public interface IBaseRepository<TEntity, TKey>
        where TEntity : BaseEntity
    {
        IQueryable<TEntity> Table();
        Task<TEntity?> FindByIdAsync(TKey id, CancellationToken token);
        Task CreateAsync(TEntity entity, CancellationToken token);
        Task CreateRangeAsync(IEnumerable<TEntity> entity, CancellationToken token);
        Task UpdateAsync(TEntity entity, CancellationToken token);
        Task UpdateRangeAsync(IEnumerable<TEntity> entity, CancellationToken token);
        Task PurgeAsync(TKey id, CancellationToken token);
        Task PurgeRangeAsync(IEnumerable<TKey> id, CancellationToken token);
    }
}
