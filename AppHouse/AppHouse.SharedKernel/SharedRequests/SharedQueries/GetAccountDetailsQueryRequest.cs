using AppHouse.SharedKernel.DTOs;
using MediatR;

namespace AppHouse.SharedKernel.SharedRequests.SharedQueries
{
    public record GetAccountDetailsQueryRequest(Guid AccountId) : IRequest<AccountDto>;
}
