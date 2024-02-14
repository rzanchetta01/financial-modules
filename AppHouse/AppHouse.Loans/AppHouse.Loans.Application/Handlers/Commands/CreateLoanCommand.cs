using AppHouse.Loans.Core.Interfaces;
using AppHouse.SharedKernel.BasicEvents;
using AppHouse.SharedKernel.DTOs;
using AppHouse.SharedKernel.SharedRequests.SharedCommands;
using MediatR;

namespace AppHouse.Loans.Application.Handlers.Commands
{
    public class CreateLoanCommand//change name to CreateLoanCommandHandler ??
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
            await _mediator.Publish(new TEntityCreated<LoanDto>(request.LoanDto), cancellationToken);
            return true;
        }
    }
}
