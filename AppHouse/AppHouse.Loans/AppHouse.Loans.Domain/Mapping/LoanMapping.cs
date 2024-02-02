﻿using AppHouse.Loans.Domain.Entities;
using AppHouse.SharedKernel.DTOs;
using Riok.Mapperly.Abstractions;

namespace AppHouse.Loans.Domain.Mapping
{
    [Mapper]
    public static partial class LoanMapping
    {
        public static partial Loan Map(LoanDto dto);
        public static partial LoanDto Map(Loan dto);
    }
}
