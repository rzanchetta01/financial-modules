using AppHouse.SharedKernel.BaseClasses;

namespace AppHouse.SharedKernel.DTOs
{ 
    public record AccountDto 
        (
            string Name,
            string Email,
            string Password,
            string Cellphone,
            string BirthDate,
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
