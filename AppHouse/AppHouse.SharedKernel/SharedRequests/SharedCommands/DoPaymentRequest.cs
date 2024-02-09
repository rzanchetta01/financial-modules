using MediatR;

namespace AppHouse.SharedKernel.SharedRequests.SharedCommands
{
    public record DoPaymentRequest(Guid From, Guid To, decimal Amount) : IRequest<bool>;
}
