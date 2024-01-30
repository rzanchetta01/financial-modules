using AppHouse.Accounts.Domain.Dto;
using MediatR;

namespace AppHouse.Accounts.Application.Requests.Commands
{
    internal record DeleteAccountRequest(Guid Id) : IRequest<bool>;
}
