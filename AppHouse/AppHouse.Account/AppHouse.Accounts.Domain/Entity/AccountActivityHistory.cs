using AppHouse.SharedKernel.BaseClasses;

namespace AppHouse.Accounts.Domain.Entity
{
    public class AccountActivityHistory : BaseEntity
    {
        public required Guid AccountId { get; set; }
        public required Guid LoanId { get; set; }
        public required bool WasPaidCorrectly { get; set; }

        public AccountActivityHistory() { }

        public AccountActivityHistory(
            Guid accountId,
            Guid loanId,
            bool wasPaidCorrectly,
            Guid? id = null,
            DateTime? dateCreated = null,
            bool? isActive = null
            ) : this(id, dateCreated, isActive)
        {
            AccountId = accountId;
            LoanId = loanId;
            WasPaidCorrectly = wasPaidCorrectly;
        }

        protected AccountActivityHistory(
            Guid? id = null,
            DateTime? dateCreated = null,
            bool? isActive = null
        )
        : base(id, dateCreated, isActive) { }
    }
}
