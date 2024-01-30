﻿using AppHouse.Accounts.Domain.Dto;
using AppHouse.SharedKernel.Interfaces;

namespace AppHouse.Accounts.Core.Interfaces
{
    public interface IAccountService : IBaseService<AccountDto, Guid>
    {

    }
}