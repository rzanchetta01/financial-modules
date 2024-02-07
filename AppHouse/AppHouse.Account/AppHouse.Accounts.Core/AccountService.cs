using AppHouse.Accounts.Core.Interfaces;
using AppHouse.Accounts.Domain.Mapping;
using AppHouse.SharedKernel.DTOs;


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

            int startAccountRating = DefineStartAccountRating(dto, token);

            var newDto = dto with { CreditScore = startAccountRating };
            await _accountRepository.CreateAsync(AccountMapping.Map(newDto), token);
        }

        public int DefineStartAccountRating(AccountDto account, CancellationToken token)
        {
            int CreditScore = 0;
            //if (account.Balance >= 6000)
                //CreditScore += 1;

            DateTime birthDate;
            if (DateTime.TryParse(account.BirthDate, out birthDate))
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
            if (account.Name.Contains(" ")) // has a surname or is included in the rule of compound names
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
        }

        public async Task Update(AccountDto dto, CancellationToken token)
        {
            await _accountRepository.CreateAsync(AccountMapping.Map(dto), token);
        }
    }

}
