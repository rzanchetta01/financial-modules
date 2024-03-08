using AppHouse.Accounts.Core.Interfaces;
using AppHouse.SharedKernel.BasicEvents;
using AppHouse.SharedKernel.DTOs;
using MediatR;

namespace AppHouse.Accounts.Application.Events
{
    public class LoanCreatedInsertIntoAccountHistoryEvent(IAccountActivityHistoryService accountActivityHistoryService) : INotificationHandler<TEventCreated<LoanDto>>
    {
        private readonly IAccountActivityHistoryService _accountActivityHistoryService = accountActivityHistoryService;
        public async Task Handle(TEventCreated<LoanDto> notification, CancellationToken cancellationToken)
        {
            if (notification.Data.Id is not null)
            {
                var dto = new AccountActivityHistoryDto
                    (
                        AccountId: notification.Data.CreatorAccountId,
                        LoanId: notification.Data.Id.Value,
                        IsReceiver: false,
                        null,
                        null,
                        null
                    );

                await _accountActivityHistoryService.Create(dto, cancellationToken);
            }
        }
    }
}
