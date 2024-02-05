using AppHouse.SharedKernel.BaseClasses;

namespace AppHouse.SharedKernel.Entities;

public partial class Loan : BaseEntity
{

    public required Guid CreatorAccountId { get; set; }

    public required decimal MaxAmount { get; set; }

    public required decimal MinAmount { get; set; }

    public required DateOnly MaxDateFeasible { get; set; }

    public required DateOnly MinDateFeasible { get; set; }

    public required int LoanStyleType { get; set; }

    public required int LoanQualityRating { get; set; }

    public string? LoanDescription { get; set; }

    public virtual ICollection<AccountActivityHistory> AccountActivityHistories { get; set; } = new List<AccountActivityHistory>();

    public virtual Account CreatorAccount { get; set; } = null!;

}
