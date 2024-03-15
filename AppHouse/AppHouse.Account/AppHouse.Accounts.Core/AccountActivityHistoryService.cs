using AppHouse.Accounts.Core.Interfaces;
using AppHouse.SharedKernel.BasicEvents;
using AppHouse.SharedKernel.DTOs;
using MediatR;

namespace AppHouse.Accounts.Core
{
    public class AccountActivityHistoryService
        (
        IAccountActivityHistoryRepository accountActivityHistoryRepository, 
        IMediator mediator
        ) : IAccountActivityHistoryService
    {
        private readonly IMediator _mediator = mediator;
        private readonly IAccountActivityHistoryRepository _accountActivityHistoryRepository = accountActivityHistoryRepository;

        public async Task Create(AccountActivityHistoryDto dto, CancellationToken token)
        {
            await _accountActivityHistoryRepository.CreateAsync(AccountMapping.Map(dto), token);
            await _mediator.Publish(new TEventCreated<AccountActivityHistoryDto>(dto), token);
        }

        public async Task<AccountActivityHistoryDto?> FindById(Guid Id, CancellationToken token)
        {
            var entity = await _accountActivityHistoryRepository.FindByIdAsync(Id, token);

            //Shoud I add the publish here??

            if (entity is not null)
                return AccountMapping.Map(entity);
            return null;
        }

        public async Task Purge(Guid id, CancellationToken token)
        {
            await _accountActivityHistoryRepository.PurgeAsync(id, token);
            await _mediator.Publish(new TEventCreated<Guid>(id), token);

        }

        public async Task Update(AccountActivityHistoryDto dto, CancellationToken token)
        {
            await _accountActivityHistoryRepository.UpdateAsync(AccountMapping.Map(dto), token);
            await _mediator.Publish(new TEventCreated<AccountActivityHistoryDto>(dto), token);
        }
    }
}
