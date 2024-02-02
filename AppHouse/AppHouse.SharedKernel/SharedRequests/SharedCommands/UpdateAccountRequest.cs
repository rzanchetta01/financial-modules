using AppHouse.SharedKernel.DTOs;
using MediatR;

namespace AppHouse.SharedKernel.SharedRequests.SharedCommands
{
    public record UpdateAccountRequest(AccountDto AccountDto) : IRequest<bool>;
}
