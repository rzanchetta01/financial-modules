using MediatR;

namespace AppHouse.SharedKernel.BasicEvents
{
    public record TEventPurged<T>(T data) : INotification;
}
