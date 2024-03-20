using AppHouse.Accounts.Core.Interfaces;
using AppHouse.SharedKernel.DTOs;
using MediatR;
using AppHouse.SharedKernel.BasicEvents;
using System.Threading;


namespace AppHouse.Accounts.Core
{

    public class AccountService
        (
        IAccountRepository accountRepository,
        IMediator mediator
        )
        : IAccountService
    {

        private readonly IAccountRepository _accountRepository = accountRepository;
        private readonly IMediator _mediator = mediator;

        public async Task Create(AccountDto dto, CancellationToken token)
        {

            int startAccountRating = DefineStartAccountRating(dto);

            var newDto = dto with { CreditScore = startAccountRating };
            await _accountRepository.CreateAsync(AccountMapping.Map(newDto), token);
            await _mediator.Publish(new TEventCreated<AccountDto>(newDto), token);
        }

        public int DefineStartAccountRating(AccountDto account)
        {
            int CreditScore = 0;
            if (account.Income >= 6000)
                CreditScore += 1;

            if (DateTime.TryParse(account.BirthDate, out DateTime birthDate))
            {
                DateTime currentDate = DateTime.Today;
                int age = currentDate.Year - birthDate.Year;

                if (birthDate.Date > currentDate.AddYears(-age))
                {
                    age--;
                }
                if (age >= 27)
                    CreditScore += 1;
            }

            if (account.AddressComplement is not null)
                CreditScore += 2;

            if (account.Name.Trim().Contains(' ')) // if name has " " it means it has more than one name
                CreditScore += 1;

            return CreditScore;
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
            await _mediator.Publish(new TEventPurged<Guid>(Id), token);
        }

        public async Task Update(AccountDto dto, CancellationToken token)
        {
            await _accountRepository.UpdateAsync(AccountMapping.Map(dto), token);
            await _mediator.Publish(new TEventUpdated<AccountDto>(dto), token);
        }
    }

}
