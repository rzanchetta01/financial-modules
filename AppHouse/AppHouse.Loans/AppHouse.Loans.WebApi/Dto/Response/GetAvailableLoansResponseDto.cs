namespace AppHouse.Loans.WebApi.Dto.Response
{
    internal record GetAvailableLoansResponseDto
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
