using AppHouse.Accounts.Core.Interfaces;
using AppHouse.SharedKernel.BasicEvents;
using AppHouse.SharedKernel.DTOs;
using MediatR;

namespace AppHouse.Accounts.Application.Events
{
    public class LoanCreatedInsertIntoAccountHistoryEvent(IAccountActivityHistoryService accountActivityHistoryService) : INotificationHandler<TEntityCreated<LoanDto>>
    {
        private readonly IAccountActivityHistoryService _accountActivityHistoryService = accountActivityHistoryService;
        public async Task Handle(TEntityCreated<LoanDto> notification, CancellationToken cancellationToken)
        {
            if (notification.Data.Id is not null)
            {
                var dto = new AccountActivityHistoryDto
                    (
                        notification.Data.CreatorAccountId,
                        notification.Data.Id.Value,
                        false,
                        null,
                        null,
                        null
                    );

                await _accountActivityHistoryService.Create(dto, cancellationToken);
            }
        }
    }
}
