using AppHouse.Accounts.Core.Interfaces;
using AppHouse.SharedKernel.BasicEvents;
using AppHouse.SharedKernel.SharedRequests.SharedCommands;
using MediatR;

namespace AppHouse.Accounts.Application.Handlers.Commands
{
    public class DeleteAccountCommandHandler
        (
            IAccountService accountService        
        )
        : IRequestHandler<DeleteAccountRequest, bool>
    {
        private readonly IAccountService _accountService = accountService;

        public async Task<bool> Handle(DeleteAccountRequest request, CancellationToken cancellationToken)
        {
            await _accountService.Purge(request.Id, cancellationToken);
<<<<<<< Updated upstream
=======

>>>>>>> Stashed changes
            return true;
        }
    }
}
