using AppHouse.Loans.Domain.Entities;

namespace AppHouse.Loans.Core.Interfaces
{
    public interface ILoanRepository : SharedKernel.Interfaces.IBaseRepository<Loan, Guid>
    {
        public Task<IEnumerable<Loan>> GetAllAvailableLoans(DateOnly maxFeasibleLoanApplyDate, CancellationToken token);
    }
}
