using AppHouse.Accounts.Domain.Dto;
using AppHouse.Accounts.Domain.Entity;
using Riok.Mapperly.Abstractions;

namespace AppHouse.Accounts.Domain.Mapping
{
    [Mapper]
    public static partial class AccountMapping
    {
        public static partial Account Map(AccountDto dto);

        public static partial AccountDto Map(Account entity);
    }
}
