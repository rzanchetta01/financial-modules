
using AppHouse.Accounts.Domain.Dto;
using MediatR;

namespace AppHouse.Accounts.Application.Requests.Commands
{
    public record CreateAccountRequest(AccountDto AccountDto) : IRequest<bool>;
}
