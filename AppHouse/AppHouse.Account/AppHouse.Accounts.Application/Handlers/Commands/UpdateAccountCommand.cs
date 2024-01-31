﻿using AppHouse.Accounts.Application.Requests.Commands;
using AppHouse.Accounts.Core.Interfaces;
using AppHouse.Accounts.Domain.Dto;
using AppHouse.SharedKernel.BasicEvents;
using MediatR;

namespace AppHouse.Accounts.Application.Handlers.Commands
{
    internal class UpdateAccountCommand
        (
            IAccountService accountService,
            IMediator mediator
        )
        : IRequestHandler<UpdateAccountRequest, bool>
    {
        private readonly IAccountService _accountService = accountService;
        private readonly IMediator _mediator = mediator;

        public async Task<bool> Handle(UpdateAccountRequest request, CancellationToken cancellationToken)
        {
            await _accountService.Update(request.AccountDto, cancellationToken);

            await _mediator.Publish(new TEntityUpdated<AccountDto>(request.AccountDto), cancellationToken);
            return true;
        }
    }
}