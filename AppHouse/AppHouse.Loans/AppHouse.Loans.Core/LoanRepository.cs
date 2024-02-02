using AppHouse.Loans.Core.Interfaces;
using AppHouse.Loans.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppHouse.Loans.Core
{
    public class LoanRepository(LoanContext loanContext) : ILoanRepository
    {
        private readonly LoanContext _loanContext = loanContext;

        public async Task CreateAsync(Loan entity, CancellationToken token)
        {
            await _loanContext.Loans.AddAsync(entity, token);
            await _loanContext.SaveChangesAsync(token);
        }

        public async Task CreateRangeAsync(IEnumerable<Loan> entities, CancellationToken token)
        {
            await _loanContext.Loans.AddRangeAsync(entities, token);
            await _loanContext.SaveChangesAsync(token);
        }

        public async Task<Loan?> FindByIdAsync(Guid id, CancellationToken token)
        {
            return await _loanContext.Loans.FindAsync([id], cancellationToken: token);
        }

        public async Task<IEnumerable<Loan>> GetAllAvailableLoans(DateOnly maxFeasibleLoanApplyDate, CancellationToken token)
        {
            return await _loanContext.Loans.Where(loan => loan.IsActive && loan.MaxDateFeasible <= maxFeasibleLoanApplyDate).OrderByDescending(o => o.DateCreated).ToListAsync(token);
        }

        public async Task PurgeAsync(Guid id, CancellationToken token)
        {
            var entity = await FindByIdAsync(id, token);
            if (entity is not null)
            {
                entity.IsActive = false;
                await _loanContext.SaveChangesAsync(token);
            }
        }

        public async Task PurgeRangeAsync(IEnumerable<Guid> ids, CancellationToken token)
        {
            await _loanContext.Loans
                .Where(e => ids.Contains(e.Id))
                .ForEachAsync(e => e.IsActive = false, token);

            await _loanContext.SaveChangesAsync(token);
        }

        public IQueryable<Loan> Table()
        {
            return _loanContext.Loans;
        }

        public async Task UpdateAsync(Loan entity, CancellationToken token)
        {
            if (entity is not null)
            {
                await _loanContext.SaveChangesAsync(token);
            }
        }

        public async Task UpdateRangeAsync(IEnumerable<Loan> entities, CancellationToken token)
        {
            if (entities.Any(e => e is null))
                return;

            await _loanContext.SaveChangesAsync(token);
        }
    }
}
