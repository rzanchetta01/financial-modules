using AppHouse.SharedKernel.DTOs;
using AppHouse.SharedKernel.Interfaces;

namespace AppHouse.Loans.Core.Interfaces
{
    public interface ILoanService : IBaseService<LoanDto, Guid>
    {
        public Task<IEnumerable<LoanDto>> GetFeasibleLoans(AccountDto accountData, DateOnly maxFeasibleLoanApplyDate, CancellationToken token);
    }
}
