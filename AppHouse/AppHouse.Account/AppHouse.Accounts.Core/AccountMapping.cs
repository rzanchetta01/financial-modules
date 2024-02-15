using AppHouse.SharedKernel.Entities;
using AppHouse.SharedKernel.DTOs;
using Riok.Mapperly.Abstractions;

namespace AppHouse.Accounts.Core
{
    [Mapper]
    public static partial class AccountMapping
    {
        public static partial Account Map(AccountDto dto);

        public static partial AccountDto Map(Account entity);

        public static partial IEnumerable<Account> Map(IEnumerable<AccountDto> dto);

        public static partial IEnumerable<AccountDto> Map(IEnumerable<Account>entity);


        public static partial AccountActivityHistory Map(AccountActivityHistoryDto dto);

        public static partial AccountActivityHistoryDto Map(AccountActivityHistory entity);

        public static partial IEnumerable<AccountActivityHistory> Map(IEnumerable<AccountActivityHistoryDto> dto);

        public static partial IEnumerable<AccountActivityHistoryDto> Map(IEnumerable<AccountActivityHistory> entity);
    }
}
