using AppHouse.Loans.Core;
using AppHouse.Loans.Core.Interfaces;
using AppHouse.SharedKernel.BasicEvents;
using MediatR;

namespace AppHouse.Loans.Application.Events
{
    public class DisposeLoanEvent(ILoanService loanService) : INotificationHandler<TEventPurged<LoanDto>>
    {
        private readonly ILoanService _loanService = loanService;

        public async Task Handle(TEventPurged<LoanDto> notification, CancellationToken cancellationToken)
        {
            if(notification.data.Id is not null)
                await _loanService.Purge(notification.data.Id.Value, cancellationToken);

                
        }
    }
}
