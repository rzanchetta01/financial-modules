using AppHouse.Loans.Application.Requests.Commands;
using FluentValidation;

namespace AppHouse.Loans.Application.Validators.Commands
{
    public class CreateLoanValidator : AbstractValidator<CreateLoanRequest>
    {
        public CreateLoanValidator() 
        {
            RuleFor(req => req.LoanDto.CreatorAccountId).NotEmpty();
            RuleFor(req => req.LoanDto.MaxAmount).NotEmpty().GreaterThan(0);
            RuleFor(req => req.LoanDto.MinAmount).NotEmpty().GreaterThan(0);

            RuleFor(req => req.LoanDto.MaxDateFeasible)
                .NotEmpty()
                .Must(BeAValidDate)
                .Must((dto, max) => BeValidMaxDateFeasible(dto.LoanDto.MinDateFeasible, max))
                .WithMessage("MaxDateFeasible should be greater than MinDateFeasible");
            
            RuleFor(req => req.LoanDto.MinDateFeasible).NotEmpty().Must(BeAValidDate);
            RuleFor(req => req.LoanDto.LoanStyleType).NotEmpty();
            RuleFor(req => req.LoanDto.LoanQualityRating).NotEmpty();
            RuleFor(req => req.LoanDto.LoanDescription);
            RuleFor(req => req.LoanDto.Id).Empty();
            RuleFor(req => req.LoanDto.DateCreated).Empty();
            RuleFor(req => req.LoanDto.IsActive).Empty();             
        }

        private bool BeAValidDate(string date) => DateOnly.TryParse(date, out _);

        private static bool BeValidMaxDateFeasible(string min, string max) => DateOnly.Parse(max) > DateOnly.Parse(min);
    }
}
