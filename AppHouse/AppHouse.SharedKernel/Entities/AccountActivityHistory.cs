﻿using AppHouse.SharedKernel.BaseClasses;

namespace AppHouse.SharedKernel.Entities;

public partial class AccountActivityHistory : BaseEntity
{

    public required Guid AccountId { get; set; }

    public required Guid LoanId { get; set; }

    public required bool IsReceiver { get; set; }

    public virtual Account Account { get; set; } = null!;

}
