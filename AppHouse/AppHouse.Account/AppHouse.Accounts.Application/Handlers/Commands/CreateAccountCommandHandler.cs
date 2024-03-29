﻿using AppHouse.Accounts.Core.Interfaces;
using AppHouse.SharedKernel.BasicEvents;
using AppHouse.SharedKernel.DTOs;
using AppHouse.SharedKernel.SharedRequests.SharedCommands;
using MediatR;

namespace AppHouse.Accounts.Application.Handlers.Commands
{
    public class CreateAccountCommandHandler
        (
            IAccountService accountService)
        : IRequestHandler<CreateAccountRequest, bool>
    {
        private readonly IAccountService _accountService = accountService;


        public async Task<bool> Handle(CreateAccountRequest request, CancellationToken cancellationToken)
        {
            await _accountService.Create(request.AccountDto, cancellationToken);
            return true;
        }               
    }
}
