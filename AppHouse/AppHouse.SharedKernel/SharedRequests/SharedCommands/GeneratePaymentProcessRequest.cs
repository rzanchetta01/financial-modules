using AppHouse.SharedKernel.DTOs;
using MediatR;

namespace AppHouse.SharedKernel.SharedRequests.SharedCommands
{
    public record GeneratePaymentProcessRequest(AccountDto Receiver, AccountDto Payer, decimal Value) : IRequest<bool>;
}
