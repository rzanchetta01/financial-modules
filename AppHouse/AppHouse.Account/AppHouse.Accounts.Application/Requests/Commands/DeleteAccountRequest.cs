using AppHouse.Accounts.Domain.Dto;
using MediatR;

namespace AppHouse.Accounts.Application.Requests.Commands
{
    public record DeleteAccountRequest(Guid Id) : IRequest<bool>;
}
