using AppHouse.SharedKernel.BaseClasses;

namespace AppHouse.SharedKernel.Entities;

public partial class AccountActivityHistory : BaseEntity
{

    public required Guid AccountId { get; set; }

    public required Guid LoanId { get; set; }

    public required bool WasPaidCorrectly { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual Loan Loan { get; set; } = null!;
}
