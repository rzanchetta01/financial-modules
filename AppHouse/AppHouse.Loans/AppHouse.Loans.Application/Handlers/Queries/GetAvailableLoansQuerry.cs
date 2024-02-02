using AppHouse.Loans.Core.Interfaces;
using AppHouse.SharedKernel.DTOs;
using AppHouse.SharedKernel.SharedRequests.SharedQueries;
using MediatR;

namespace AppHouse.Loans.Application.Handlers.Queries
{
    public class GetAvailableLoansQuery(
        ILoanService loanService,
        IMediator mediator
        ) : IRequestHandler<GetAvailableLoanRequest, IEnumerable<LoanDto>>
    { 
        private readonly ILoanService _loanService = loanService;
        private readonly IMediator _mediator = mediator;

        public async Task<IEnumerable<LoanDto>> Handle(GetAvailableLoanRequest request, CancellationToken cancellationToken)
        {
            //it needs to call the GetAccountDetailsRequest from the AppHouse.Account where here it cannot have the project reference
            return await _loanService.GetFeasibleLoans(DateOnly.Parse(request.MaxFeasibleDate), cancellationToken);
        }
    }
}
