﻿using AppHouse.Accounts.Core.Interfaces;
using AppHouse.SharedKernel.BasicEvents;
using AppHouse.SharedKernel.SharedRequests.SharedCommands;
using MediatR;

namespace AppHouse.Accounts.Application.Handlers.Commands
{
    public class DeleteAccountCommand
        (
            IAccountService accountService,
            IMediator mediator
        )
        : IRequestHandler<DeleteAccountRequest, bool>
    {
        private readonly IAccountService _accountService = accountService;
        private readonly IMediator _mediator = mediator;

        public async Task<bool> Handle(DeleteAccountRequest request, CancellationToken cancellationToken)
        {
            await _accountService.Purge(request.Id, cancellationToken);

            await _mediator.Publish(new TEntityPurged<Guid>(request.Id), cancellationToken);
            return true;
        }
    }
}
