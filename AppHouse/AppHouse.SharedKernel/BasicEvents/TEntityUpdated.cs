using MediatR;

namespace AppHouse.SharedKernel.BasicEvents
{
    public record TEntityUpdated<TEntity>(TEntity Entity) : INotification;
}
