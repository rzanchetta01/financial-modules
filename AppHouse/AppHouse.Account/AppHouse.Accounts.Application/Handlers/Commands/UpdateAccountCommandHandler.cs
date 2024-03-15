using AppHouse.Accounts.Core.Interfaces;
using AppHouse.SharedKernel.BasicEvents;
using AppHouse.SharedKernel.DTOs;
using AppHouse.SharedKernel.SharedRequests.SharedCommands;
using MediatR;

namespace AppHouse.Accounts.Application.Handlers.Commands
{
    public class UpdateAccountCommandHandler
        (
            IAccountService accountService
        )
        : IRequestHandler<UpdateAccountRequest, bool>
    {
        private readonly IAccountService _accountService = accountService;

        public async Task<bool> Handle(UpdateAccountRequest request, CancellationToken cancellationToken)
        {
            await _accountService.Update(request.AccountDto, cancellationToken);
<<<<<<< Updated upstream

=======
>>>>>>> Stashed changes
            return true;
        }
    }
}
