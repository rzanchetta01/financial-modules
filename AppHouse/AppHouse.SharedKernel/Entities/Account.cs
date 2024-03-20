using AppHouse.SharedKernel.BaseClasses;

namespace AppHouse.SharedKernel.Entities;

public partial class Account : BaseEntity
{

    public required string Name { get; set; }

    public required string Email { get; set; }

    public required string Password { get; set; }

    public required string Cellphone { get; set; }

    public required DateOnly BirthDate { get; set; }

    public required string Country { get; set; }

    public required string State { get; set; }

    public required string City { get; set; }

    public required string PostalCode { get; set; }

    public required string Address { get; set; }

    public string? AddressComplement { get; set; }

    public required decimal Income { get; set; }

    public required double CreditScore { get; set; }

    public virtual ICollection<AccountActivityHistory> AccountActivityHistories { get; set; } = [];

}
