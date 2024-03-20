using AppHouse.SharedKernel.BaseClasses;

namespace AppHouse.Loans.Core
{
    public record LoanDto(
        Guid CreatorAccountId,
        decimal MaxAmount,
        decimal MinAmount,
        string MaxDateFeasible,
        string MinDateFeasible,
        int LoanStyleType,
        int LoanQualityRating,
        string? LoanDescription,
        Guid? Id,
        DateTime? DateCreated,
        bool? IsActive
        ) : BaseDto(Id, DateCreated, IsActive);
}
