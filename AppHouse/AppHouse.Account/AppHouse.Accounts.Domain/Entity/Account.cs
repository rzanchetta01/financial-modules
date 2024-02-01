using AppHouse.SharedKernel.BaseClasses;
using System.Diagnostics.CodeAnalysis;

namespace AppHouse.Accounts.Domain.Entity
{

    public class Account : BaseEntity
    {
        public Account() // Parameterless constructor
        {
        }

        public Account(
            string name,
            string email,
            string password,
            string cellphone,
            DateOnly birthDate,
            string country,
            string state,
            string city,
            string postalCode,
            string address,
            string? addressComplement,
            decimal income,
            double creditScore,
            Guid? id = null,
            DateTime? dateCreated = null,
            bool? isActive = null
        ) : this(id, dateCreated, isActive)
        {
            Name = name;
            Email = email;
            Password = password;
            Cellphone = cellphone;
            BirthDate = birthDate;
            Country = country;
            State = state;
            City = city;
            PostalCode = postalCode;
            Address = address;
            AddressComplement = addressComplement;
            Income = income;
            CreditScore = creditScore;
        }

        protected Account(Guid? id = null, DateTime? dateCreated = null, bool? isActive = null)
            : base(id, dateCreated, isActive)
        {
        }

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
        public required string? AddressComplement { get; set; }
        public required decimal Income { get; set; }
        public required double CreditScore { get; set; }
    }

}

