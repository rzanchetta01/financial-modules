using AppHouse.Loans.Domain.Entities;
using AppHouse.SharedKernel.BaseClasses;

namespace AppHouse.Loans.Domain.Dtos
{
    public record LoanDto(
        Guid CreatorAccountId,
        decimal MaxAmmount,
        decimal MinAmmount,
        string MaxDateFeasible,
        string MinDateFeasible,
        LoanStyleType LoanStyleType,
        string? LoanDescription,
        Guid? Id,
        DateTime? DateCreated,
        bool? IsActive
        ) : BaseDto(Id, DateCreated, IsActive);
}
