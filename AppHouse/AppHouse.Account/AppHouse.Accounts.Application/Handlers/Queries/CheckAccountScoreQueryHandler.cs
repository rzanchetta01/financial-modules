
using AppHouse.Accounts.Core.Interfaces;
using AppHouse.SharedKernel.SharedRequests.SharedQueries;
using MediatR;

namespace AppHouse.Accounts.Application.Handlers.Queries
{
    internal class CheckAccountScoreQueryHandler(IAccountService accountService) : IRequestHandler<CheckAccountScoreQueryRequest, int>
    {
        private readonly IAccountService _accountService = accountService;

        public async Task<int> Handle(CheckAccountScoreQueryRequest request, CancellationToken cancellationToken)
        {
            var account = await _accountService.FindById(request.AccountId, cancellationToken) ?? throw new ArgumentException("Account not found");
            return (int)Math.Floor(account.CreditScore);
        }
    }
}
