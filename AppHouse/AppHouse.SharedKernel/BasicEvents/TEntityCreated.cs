using MediatR;

namespace AppHouse.SharedKernel.BasicEvents
{
    public record TEntityCreated<T>(T Data) : INotification;
}
