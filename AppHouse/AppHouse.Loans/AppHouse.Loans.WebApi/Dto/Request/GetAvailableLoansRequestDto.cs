namespace AppHouse.Loans.WebApi.Dto.Request
{
    public record GetAvailableLoansRequestDto(string MaxDate, Guid AccountId, decimal Income, int CreditScore);
}
