using AppHouse.Loans.Domain.Entities;

namespace AppHouse.Loans.Core.Interfaces
{
    public interface ILoanRepository : SharedKernel.Interfaces.IBaseRepository<Loan, Guid>
    {
    }
}
