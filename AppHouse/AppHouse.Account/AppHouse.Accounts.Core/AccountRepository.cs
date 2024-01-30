using AppHouse.SharedKernel;
using AppHouse.Accounts.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using AppHouse.Accounts.Core.Interfaces;

namespace AppHouse.Accounts.Core
{
    internal class AccountRepository(AccountsContext context) : IAccountRepository
    {
        private readonly AccountsContext _context = context;

        public async Task CreateAsync(Account entity, CancellationToken token)
        {
            await _context.Accounts.AddAsync(entity, token);
        }

        public async Task CreateRangeAsync(IEnumerable<Account> entities, CancellationToken token)
        {
            await _context.Accounts.AddRangeAsync(entities, token);
        }

        public async Task<Account?> FindByIdAsync(Guid id, CancellationToken token)
        {
            return await _context.Accounts.FindAsync([id], cancellationToken: token);
        }

        public async Task PurgeAsync(Guid id, CancellationToken token)
        {
            var entity = await FindByIdAsync(id, token);
            if(entity is not null)
            {
                entity.IsActive = false;
                await _context.SaveChangesAsync(token);
            }
        }

        public async Task PurgeRangeAsync(IEnumerable<Guid> ids, CancellationToken token)
        {
            await _context.Accounts
                .Where(e => ids.Contains(e.Id))
                .ForEachAsync(e => e.IsActive = false, token);
            
            await _context.SaveChangesAsync(token);
        }

        public IQueryable<Account> Table()
        {
            return _context.Accounts;
        }

        public async Task UpdateAsync(Account entity, CancellationToken token)
        {
            if(entity is not null)
            {
                await _context.SaveChangesAsync(token);
            }
        }

        public async Task UpdateRangeAsync(IEnumerable<Account> entities, CancellationToken token)
        {
            if (entities.Any(e => e is null))
                return;

            await _context.SaveChangesAsync(token);
        }
    }
}
