using AppHouse.SharedKernel.SharedRequests.SharedQueries;
using FluentValidation;


namespace AppHouse.Accounts.Application.Validators
{
    public class GetAccountDetailsValidator : AbstractValidator<GetAccountDetailsQueryRequest>
    {
        public GetAccountDetailsValidator()
        {
            RuleFor(req => req.AccountId).NotEmpty().NotEqual(Guid.Empty);
        }
    }
}
