﻿using AppHouse.SharedKernel.Entities;
using AppHouse.SharedKernel.DTOs;
using Riok.Mapperly.Abstractions;

namespace AppHouse.Accounts.Domain.Mapping
{
    [Mapper]
    public static partial class AccountMapping
    {
        public static partial Account Map(AccountDto dto);

        public static partial AccountDto Map(Account entity);

        public static partial IEnumerable<Account> Map(IEnumerable<AccountDto> dto);

        public static partial IEnumerable<AccountDto> Map(IEnumerable<Account>entity);


    }
}
