using AppHouse.Loans.Application.Requests.Commands;
using AppHouse.Loans.Core.Interfaces;
using MediatR;

namespace AppHouse.Loans.Application.Handlers.Commands
{
    public class CreateLoanCommandHandler
        (
            ILoanService loanService
        )
        : IRequestHandler<CreateLoanRequest, bool>
    {
        private readonly ILoanService _loanService = loanService;

        public async Task<bool> Handle(CreateLoanRequest request, CancellationToken cancellationToken)
        {
            await _loanService.Create(request.LoanDto, cancellationToken);
            return true;
        }
    }
}
