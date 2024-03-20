namespace AppHouse.Loans.WebApi.Dto.Request
{
    public record CreateLoanRequestDto
        (
            Guid CreatorAccountId,
            decimal MaxAmount,
            decimal MinAmount,
            string MaxDateFeasible,
            string MinDateFeasible,
            int LoanStyleType,
            int LoanQualityRating,
            string? LoanDescription
        );
}
