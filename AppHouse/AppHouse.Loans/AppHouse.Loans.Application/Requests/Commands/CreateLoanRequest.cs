using AppHouse.Loans.Core;
using MediatR;

namespace AppHouse.Loans.Application.Requests.Commands
{
    public record CreateLoanRequest(LoanDto LoanDto) : IRequest<bool>;
}
