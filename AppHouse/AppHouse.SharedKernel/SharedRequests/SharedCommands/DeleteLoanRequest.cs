using MediatR;

namespace AppHouse.SharedKernel.SharedRequests.SharedCommands
{
    public record DeleteLoanRequest(Guid LoanId) : IRequest<bool>;
}
