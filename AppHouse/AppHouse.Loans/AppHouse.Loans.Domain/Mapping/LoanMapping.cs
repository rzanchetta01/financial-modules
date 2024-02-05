using AppHouse.SharedKernel.DTOs;
using AppHouse.SharedKernel.Entities;
using Riok.Mapperly.Abstractions;

namespace AppHouse.Loans.Domain.Mapping
{
    [Mapper]
    public static partial class LoanMapping
    {
        public static partial Loan Map(LoanDto dto);
        public static partial LoanDto Map(Loan entities);

        public static partial IEnumerable<Loan> Map(IEnumerable<LoanDto> dtos);
        public static partial IEnumerable<LoanDto> Map(IEnumerable<Loan> entities);
    }
}
