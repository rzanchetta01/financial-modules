using AppHouse.SharedKernel.DTOs;
using MediatR;

namespace AppHouse.SharedKernel.SharedRequests.SharedQueries
{
    public record GetAvailableLoanRequest(Guid AccountId, string MaxFeasibleDate) : IRequest<IEnumerable<LoanDto>>;
}
