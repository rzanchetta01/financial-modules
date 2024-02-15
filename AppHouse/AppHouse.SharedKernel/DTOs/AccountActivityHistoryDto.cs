using AppHouse.SharedKernel.BaseClasses;

namespace AppHouse.SharedKernel.DTOs
{
    public record AccountActivityHistoryDto
        (
        Guid AccountId,
        Guid LoanId,
        bool IsReceiver,
        Guid? Id,
        DateTime? DateCreated,
        bool? IsActive
        ) : BaseDto(Id, DateCreated, IsActive);
}
