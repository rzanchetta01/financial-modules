using AppHouse.SharedKernel.Interfaces;

namespace AppHouse.Loans.Core.Interfaces
{
    public interface ILoanService : IBaseService<Domain.Dtos.LoanDto, Guid>
    {
    }
}
