using AppHouse.SharedKernel.SharedRequests.SharedQueries;
using FluentValidation;

namespace AppHouse.Accounts.Application.Validators
{
    public class CheckAccountScoreValidator : AbstractValidator<CheckAccountScoreQueryRequest>
    {
        public CheckAccountScoreValidator()
        {
            RuleFor(req => req.AccountId).NotEmpty().NotEqual(Guid.Empty);
        }
    }
}
