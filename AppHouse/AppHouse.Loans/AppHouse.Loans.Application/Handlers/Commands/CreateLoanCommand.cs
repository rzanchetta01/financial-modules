using AppHouse.Loans.Core.Interfaces;
using AppHouse.SharedKernel.SharedRequests.SharedCommands;
using MediatR;

namespace AppHouse.Loans.Application.Handlers.Commands
{
    public class CreateLoanCommandHandler
        (
            ILoanService loanService,
            IMediator mediator
        )
        : IRequestHandler<CreateLoanRequest, bool>
    {
        private readonly ILoanService _loanService = loanService;
        private readonly IMediator _mediator = mediator;

        public async Task<bool> Handle(CreateLoanRequest request, CancellationToken cancellationToken)
        {
            await _loanService.Create(request.LoanDto, cancellationToken);
            return true;
        }
    }
}
