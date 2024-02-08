using AppHouse.SharedKernel.SharedRequests.SharedCommands;
using FluentValidation;

namespace AppHouse.Accounts.Application.Validators
{
    public class DeleteAccountValidator : AbstractValidator<DeleteAccountRequest>
    {
        public DeleteAccountValidator() 
        { 
            RuleFor(req => req.Id).NotEmpty();
        }
    }
}
