using MediatR;

namespace AppHouse.SharedKernel.SharedRequests.SharedCommands
{
    public record DeleteAccountRequest(Guid Id) : IRequest<bool>;
}
