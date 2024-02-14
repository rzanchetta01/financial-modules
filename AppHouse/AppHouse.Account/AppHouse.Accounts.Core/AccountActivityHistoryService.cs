using AppHouse.Accounts.Core.Interfaces;
using AppHouse.SharedKernel.DTOs;

namespace AppHouse.Accounts.Core
{
    public class AccountActivityHistoryService(IAccountActivityHistoryRepository accountActivityHistoryRepository) : IAccountActivityHistoryService
    {
        private readonly IAccountActivityHistoryRepository _accountActivityHistoryRepository = accountActivityHistoryRepository;

        public async Task Create(AccountActivityHistoryDto dto, CancellationToken token)
        {
            await _accountActivityHistoryRepository.CreateAsync(AccountMapping.Map(dto), token);
        }

        public async Task<AccountActivityHistoryDto?> FindById(Guid Id, CancellationToken token)
        {
            var entity = await _accountActivityHistoryRepository.FindByIdAsync(Id, token);
            if (entity is not null)
                return AccountMapping.Map(entity);
            return null;
        }

        public async Task Purge(Guid id, CancellationToken token)
        {
            await _accountActivityHistoryRepository.PurgeAsync(id, token);
        }

        public async Task Update(AccountActivityHistoryDto dto, CancellationToken token)
        {
            await _accountActivityHistoryRepository.UpdateAsync(AccountMapping.Map(dto), token);
        }
    }
}
