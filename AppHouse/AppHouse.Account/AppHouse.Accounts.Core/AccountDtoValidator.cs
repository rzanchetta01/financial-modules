using AppHouse.Accounts.Domain.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppHouse.Accounts.Core
{
    public class AccountDtoValidator : AbstractValidator<AccountDto>
    {
        public AccountDtoValidator()
        {
            RuleFor(dto => dto.Name).NotEmpty().MaximumLength(100);
            RuleFor(dto => dto.Email).NotEmpty().EmailAddress();
            RuleFor(dto => dto.Password).NotEmpty().MinimumLength(8);
            RuleFor(dto => dto.Cellphone).NotEmpty();//.Matches(@"^\d{10}$"); // Assuming 10 digits for simplicity
            RuleFor(dto => dto.BirthDate).NotEmpty().Must(BeAValidDate);
            RuleFor(dto => dto.Country).NotEmpty();
            RuleFor(dto => dto.State).NotEmpty();
            RuleFor(dto => dto.City).NotEmpty();
            RuleFor(dto => dto.PostalCode).NotEmpty();//.Matches(@"^\d{8}$");
            RuleFor(dto => dto.Address).NotEmpty();
            RuleFor(dto => dto.Income).NotEmpty().GreaterThanOrEqualTo(0);
            RuleFor(dto => dto.CreditScore).NotEmpty().GreaterThanOrEqualTo(0);
        }

        private bool BeAValidDate(string date)
        {
            return DateTime.TryParse(date, out _);
        }
    }
}
