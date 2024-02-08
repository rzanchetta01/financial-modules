using AppHouse.Accounts.Core.Interfaces;
using AppHouse.SharedKernel.DTOs;
using AppHouse.SharedKernel.SharedRequests.SharedQueries;
using MediatR;

namespace AppHouse.Accounts.Application.Handlers.Queries
{
    public class GetAccountDetailsQueryHandler(IAccountService service) : IRequestHandler<GetAccountDetailsQueryRequest, AccountDto>
    {
        private readonly IAccountService _service = service;
        public async Task<AccountDto> Handle(GetAccountDetailsQueryRequest request, CancellationToken cancellationToken)
        {
            var result = await _service.FindById(request.AccountId, cancellationToken) ?? throw new ArgumentException("Account not found");
            return result;
        }
    }
}
