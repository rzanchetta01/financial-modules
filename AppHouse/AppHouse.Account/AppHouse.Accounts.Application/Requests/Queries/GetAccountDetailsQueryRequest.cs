using AppHouse.Accounts.Domain.Dto;
using MediatR;

namespace AppHouse.Accounts.Application.Requests.Queries
{
    public record GetAccountDetailsQueryRequest(Guid AccountId) : IRequest<AccountDto?>;
}
