using AppHouse.Accounts.Core.Interfaces;
using AppHouse.Accounts.Domain.Dto;
using AppHouse.Accounts.Domain.Entity;
using AppHouse.SharedKernel;
using FluentValidation;
using Mapster;

namespace AppHouse.Accounts.Core
{
    public class AccountService
        (
        IAccountRepository accountRepository,
        IValidator<AccountDto> accountDtoValidator
        )
        : IAccountService
    {
        private readonly IAccountRepository _accountRepository = accountRepository;
        private readonly IValidator<AccountDto> _accountDtoValidator = accountDtoValidator;

        public async Task Create(AccountDto dto, CancellationToken token)
        {
            var valid = await _accountDtoValidator.ValidateAsync(dto, token);
            if (valid.IsValid)
                await _accountRepository.CreateAsync(dto.Adapt<Account>(), token);
        }

        public async Task<AccountDto> FindById(Guid Id, CancellationToken token)
        {
            var entity = await _accountRepository.FindByIdAsync(Id, token);
            return entity.Adapt<AccountDto>();
        }

        public async Task Purge(Guid Id, CancellationToken token)
        {
            await _accountRepository.PurgeAsync(Id, token);
        }

        public async Task Update(AccountDto dto, CancellationToken token)
        {
            var valid = await _accountDtoValidator.ValidateAsync(dto, token);
            if (valid.IsValid)
            {
                await _accountRepository.CreateAsync(dto.Adapt<Account>(), token);
            }
        }
    }
}
