﻿using AppHouse.Accounts.Core.Interfaces;
using AppHouse.Accounts.Domain.Dto;
using AppHouse.Accounts.Domain.Mapping;
using FluentValidation;

namespace AppHouse.Accounts.Core
{
    public class AccountService
        (
        IAccountRepository accountRepository
        )
        : IAccountService
    {
        private readonly IAccountRepository _accountRepository = accountRepository;

        public async Task Create(AccountDto dto, CancellationToken token)
        {
            await _accountRepository.CreateAsync(AccountMapping.Map(dto), token);
        }

        public async Task<AccountDto?> FindById(Guid Id, CancellationToken token)
        {
            var entity = await _accountRepository.FindByIdAsync(Id, token);
            if(entity is not null)
                return AccountMapping.Map(entity);

            return null;
        }

        public async Task Purge(Guid Id, CancellationToken token)
        {
            await _accountRepository.PurgeAsync(Id, token);
        }

        public async Task Update(AccountDto dto, CancellationToken token)
        {
            await _accountRepository.CreateAsync(AccountMapping.Map(dto), token);
        }
    }
}
