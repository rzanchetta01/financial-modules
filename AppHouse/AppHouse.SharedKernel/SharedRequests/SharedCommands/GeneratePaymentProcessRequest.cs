using AppHouse.SharedKernel.DTOs;
using MediatR;

namespace AppHouse.SharedKernel.SharedRequests.SharedCommands
{
    public record GeneratePaymentProcessRequest(AccountDto Receiver, AccountDto Payer, LoanDto LoanDto) : IRequest<bool>;
}
