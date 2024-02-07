using AppHouse.SharedKernel.Entities;
using Microsoft.EntityFrameworkCore;
using AppHouse.Accounts.Core.Interfaces;

namespace AppHouse.Accounts.Core
{
    public class AccountRepository(AccountsContext context) : IAccountRepository
    {
        private readonly AccountsContext _accountContext = context;

        public async Task CreateAsync(Account entity, CancellationToken token)
        {
            await _accountContext.Accounts.AddAsync(entity, token);
            await _accountContext.SaveChangesAsync(token);
        }

        public async Task CreateRangeAsync(IEnumerable<Account> entities, CancellationToken token)
        {
            await _accountContext.Accounts.AddRangeAsync(entities, token);
            await _accountContext.SaveChangesAsync(token);
        }

        public async Task<Account?> FindByIdAsync(Guid id, CancellationToken token)
        {
            return await _accountContext.Accounts.FindAsync([id], cancellationToken: token);
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
            await _accountContext.Accounts
                .Where(e => ids.Contains(e.Id))
                .ForEachAsync(e => _accountContext.Remove(e), token);
            
            await _accountContext.SaveChangesAsync(token);
        }

        public IQueryable<Account> Table()
        {
            return _accountContext.Accounts;
        }

        public async Task UpdateAsync(Account entity, CancellationToken token)
        {
            if(entity is not null)
            {
                await _accountContext.SaveChangesAsync(token);
            }
        }

        public async Task UpdateRangeAsync(IEnumerable<Account> entities, CancellationToken token)
        {
            if (entities.Any(e => e is null))
                return;

            await _accountContext.SaveChangesAsync(token);
        }
    }
}
