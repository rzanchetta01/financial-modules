using AppHouse.Accounts.Core;
using AppHouse.Accounts.Core.Interfaces;
using AppHouse.SharedKernel.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppHouse.AccountActivityHistories.Core
{
    public class AccountActivityHistoryRepository(AccountsContext accountContext) : IAccountActivityHistoryRepository
    {
        private readonly AccountsContext _accountContext = accountContext;
        public async Task CreateAsync(AccountActivityHistory entity, CancellationToken token)
        {
            await _accountContext.AccountActivityHistories.AddAsync(entity, token);
            await _accountContext.SaveChangesAsync(token);
        }

        public async Task CreateRangeAsync(IEnumerable<AccountActivityHistory> entities, CancellationToken token)
        {
            await _accountContext.AccountActivityHistories.AddRangeAsync(entities, token);
            await _accountContext.SaveChangesAsync(token);
        }

        public async Task<AccountActivityHistory?> FindByIdAsync(Guid id, CancellationToken token)
        {
            return await _accountContext.AccountActivityHistories.FindAsync([id], cancellationToken: token);
        }

        public async Task PurgeAsync(Guid id, CancellationToken token)
        {
            var entity = await FindByIdAsync(id, token);
            if (entity is null)
                return;

            _accountContext.Remove(entity);
            await _accountContext.SaveChangesAsync(token);
        }

        public async Task PurgeRangeAsync(IEnumerable<Guid> ids, CancellationToken token)
        {
            await _accountContext.AccountActivityHistories
                .Where(e => ids.Contains(e.Id))
                .ForEachAsync(e => _accountContext.Remove(e), token);

            await _accountContext.SaveChangesAsync(token);
        }

        public IQueryable<AccountActivityHistory> Table()
        {
            return _accountContext.AccountActivityHistories;
        }

        public async Task UpdateAsync(AccountActivityHistory entity, CancellationToken token)
        {
            if (entity is not null)
            {
                await _accountContext.SaveChangesAsync(token);
            }
        }

        public async Task UpdateRangeAsync(IEnumerable<AccountActivityHistory> entities, CancellationToken token)
        {
            if (entities.Any(e => e is null))
                return;

            await _accountContext.SaveChangesAsync(token);
        }
    }
}
