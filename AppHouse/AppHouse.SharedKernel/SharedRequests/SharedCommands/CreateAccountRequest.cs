using AppHouse.SharedKernel.DTOs;
using MediatR;

namespace AppHouse.SharedKernel.SharedRequests.SharedCommands
{
    public record CreateAccountRequest(AccountDto AccountDto) : IRequest<bool>;
}
