using AppHouse.Loans.Core;
using MediatR;

namespace AppHouse.Loans.Application.Requests.Queries
{
    public record GetAvailableLoanRequest(Guid AccountId, string MaxFeasibleDate, decimal Income, int CreditScore) : IRequest<IEnumerable<LoanDto>>;
}
