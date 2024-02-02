using AppHouse.Accounts.Application.Requests.Commands;
using FluentValidation;

namespace AppHouse.Accounts.Application.Validators
{
    public class UpdateAccountValidator : AbstractValidator<UpdateAccountRequest>
    {
        public UpdateAccountValidator()
        {
            RuleFor(req => req.AccountDto.Name).NotEmpty().MaximumLength(100);
            RuleFor(req => req.AccountDto.Email).NotEmpty().EmailAddress();
            RuleFor(req => req.AccountDto.Password).NotEmpty().MinimumLength(8);
            RuleFor(req => req.AccountDto.Cellphone).NotEmpty();//.Matches(@"^\d{10}$"); // Assuming 10 digits for simplicity
            RuleFor(req => req.AccountDto.BirthDate).NotEmpty().Must(BeAValidDate);
            RuleFor(req => req.AccountDto.Country).NotEmpty();
            RuleFor(req => req.AccountDto.State).NotEmpty();
            RuleFor(req => req.AccountDto.City).NotEmpty();
            RuleFor(req => req.AccountDto.PostalCode).NotEmpty();//.Matches(@"^\d{8}$");
            RuleFor(req => req.AccountDto.Address).NotEmpty();
            RuleFor(req => req.AccountDto.Income).NotEmpty().GreaterThanOrEqualTo(0);
            RuleFor(req => req.AccountDto.CreditScore).NotEmpty().GreaterThanOrEqualTo(0);
        }

        private bool BeAValidDate(string date) => DateTime.TryParse(date, out _);
    }
}
