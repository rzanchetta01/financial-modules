using AppHouse.SharedKernel.DTOs;
using AppHouse.SharedKernel.Interfaces;

namespace AppHouse.Loans.Core.Interfaces
{
    public interface ILoanService : IBaseService<LoanDto, Guid>
    {
        public Task<IEnumerable<LoanDto>> GetFeasibleLoans(decimal income, int creditScore, DateOnly maxFeasibleLoanApplyDate, CancellationToken token);
    }
}
