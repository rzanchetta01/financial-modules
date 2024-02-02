using AppHouse.Accounts.Application.Requests.Queries;
using AppHouse.Accounts.Core.Interfaces;
using AppHouse.Accounts.Domain.Dto;
using MediatR;

namespace AppHouse.Accounts.Application.Handlers.Queries
{
    public class GetAccountDetailsQueryHandler(IAccountService service) : IRequestHandler<GetAccountDetailsQueryRequest, AccountDto?>
    {
        private IAccountService _service = service;
        public async Task<AccountDto?> Handle(GetAccountDetailsQueryRequest request, CancellationToken cancellationToken)
        {
            return await _service.FindById(request.AccountId, cancellationToken);
        }
    }
}
