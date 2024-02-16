using MediatR;

namespace AppHouse.SharedKernel.BasicEvents
{
    public record TEventUpdated<T>(T data) : INotification;
}
