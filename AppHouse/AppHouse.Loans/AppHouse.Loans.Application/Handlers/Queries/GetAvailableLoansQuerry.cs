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
            var accountDetails = await _mediator.Send(new GetAccountDetailsQueryRequest(request.AccountId), cancellationToken);

            if(accountDetails is not null)
                return await _loanService.GetFeasibleLoans(accountDetails, DateOnly.Parse(request.MaxFeasibleDate), cancellationToken);

            return [];
        }
    }
}
