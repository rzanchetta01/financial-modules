using AppHouse.SharedKernel.BaseClasses;
using System.Text.Json.Serialization;

namespace AppHouse.Accounts.Domain.Dto
{
    public record AccountDto 
        (
            string Name,
            string Email,
            string Password,
            string Cellphone,
            DateTime BirthDate,
            string Country,
            string State,
            string City,
            string PostalCode,
            string Address,
            string? AddressComplement,
            decimal Income,
            double CreditScore,
            Guid? Id,
            DateTime? DateCreated,
            bool? IsActive
        ) : BaseDto(Id, DateCreated, IsActive);
}
