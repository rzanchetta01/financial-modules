using AppHouse.SharedKernel.SharedRequests.SharedCommands;
using FluentValidation;

namespace AppHouse.Accounts.Core
{
    public class CreateAccountValidator : AbstractValidator<CreateAccountRequest>
    {
        public CreateAccountValidator()
        {
            RuleFor(req => req.AccountDto.Name).NotEmpty().MaximumLength(100);
            RuleFor(req => req.AccountDto.Email).NotEmpty().EmailAddress();
            RuleFor(req => req.AccountDto.Password).NotEmpty().MinimumLength(8);
            RuleFor(req => req.AccountDto.Cellphone).Matches(@"^\d{10}$"); // Assuming 10 digits for simplicity
            RuleFor(req => req.AccountDto.BirthDate).NotEmpty().Must(BeAValidDate);
            RuleFor(req => req.AccountDto.Country).NotEmpty();
            RuleFor(req => req.AccountDto.State).NotEmpty();
            RuleFor(req => req.AccountDto.City).NotEmpty();
            RuleFor(req => req.AccountDto.PostalCode).Matches(@"^\d{8}$");// Eight digits similar to Brazil CEP
            RuleFor(req => req.AccountDto.Address).NotEmpty();
            RuleFor(req => req.AccountDto.Income).NotEmpty().GreaterThanOrEqualTo(0);
            RuleFor(req => req.AccountDto.CreditScore).NotEmpty().GreaterThanOrEqualTo(0);
            RuleFor(req => req.AccountDto.Id).Empty();          //Should be null in new accounts
            RuleFor(req => req.AccountDto.DateCreated).Empty(); //Should be null in new accounts
            RuleFor(req => req.AccountDto.IsActive).Empty();    //Should be null in new accounts
        }

        private bool BeAValidDate(string date) => DateOnly.TryParse(date, out _);
    }
}
