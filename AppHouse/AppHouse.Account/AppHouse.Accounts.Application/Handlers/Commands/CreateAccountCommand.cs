﻿using AppHouse.Accounts.Core.Interfaces;
using AppHouse.SharedKernel.BasicEvents;
using AppHouse.SharedKernel.DTOs;
using AppHouse.SharedKernel.SharedRequests.SharedCommands;
using MediatR;

namespace AppHouse.Accounts.Application.Handlers.Commands
{
    public class CreateAccountCommand
        (
            IAccountService accountService,
            IMediator mediator 
        )
        : IRequestHandler<CreateAccountRequest, bool>
    {
        private readonly IAccountService _accountService = accountService;
        private readonly IMediator _mediator = mediator;


        public async Task<bool> Handle(CreateAccountRequest request, CancellationToken cancellationToken)
        {
            await _accountService.Create(request.AccountDto, cancellationToken);
            await _mediator.Publish(new TEntityCreated<AccountDto>(request.AccountDto), cancellationToken);
            return true;
        }               
    }
}
