using AppHouse.SharedKernel.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppHouse.SharedKernel.DTOs
{
    public record AccountActivityHistoryDto
        (
        Guid AccountId,
        Guid LoanId,
        bool WasPaidCorrectly,
        Guid? Id,
        DateTime? DateCreated,
        bool? IsActive
        ) : BaseDto(Id, DateCreated, IsActive);
}
