using AppHouse.Loans.Application.Requests.Queries;
using AppHouse.Loans.Core;
using AppHouse.Loans.Core.Interfaces;
using MediatR;

namespace AppHouse.Loans.Application.Handlers.Queries
{
    public class GetAvailableLoansQuery(
        ILoanService loanService
        ) : IRequestHandler<GetAvailableLoanRequest, IEnumerable<LoanDto>>
    { 
        private readonly ILoanService _loanService = loanService;

        public async Task<IEnumerable<LoanDto>> Handle(GetAvailableLoanRequest request, CancellationToken cancellationToken)
        {
            return await _loanService.GetFeasibleLoans(request.Income, request.CreditScore, DateOnly.Parse(request.MaxFeasibleDate), cancellationToken);
        }
    }
}
