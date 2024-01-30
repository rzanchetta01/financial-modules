using MediatR;

namespace AppHouse.SharedKernel.BasicEvents
{
    public record TEntityPurged<TEntity>(TEntity Entity) : INotification;
}
