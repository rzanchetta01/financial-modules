using AppHouse.SharedKernel.BaseClasses;

namespace AppHouse.Loans.Domain.Entities
{
    public class Loan : BaseEntity
    {
        public required Guid CreatorAccountId { get; set; }
        public required decimal MaxAmmount { get; set; }
        public required decimal MinAmmount { get; set; }
        public required DateOnly MaxDateFeasible { get; set; }
        public required DateOnly MinDateFeasible { get; set; }
        public required LoanStyleType LoanStyleType { get; set; }
        public string? LoanDescription { get; set; }
    }
}
