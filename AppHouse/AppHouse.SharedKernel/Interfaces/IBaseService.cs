using AppHouse.SharedKernel.BaseClasses;

namespace AppHouse.SharedKernel.Interfaces
{
    public interface IBaseService<TDto, TKey>
        where TDto : BaseDto
    {
        Task Create(TDto dto, CancellationToken token);
        Task Update(TDto dto, CancellationToken token);
        Task Purge(TKey dto, CancellationToken token);
        Task<TDto?> FindById(TKey Id, CancellationToken token);
    }
}
