using AppHouse.SharedKernel.DTOs;
using AppHouse.SharedKernel.Interfaces;

namespace AppHouse.Accounts.Core.Interfaces
{
    public interface IAccountService : IBaseService<AccountDto, Guid>
    {
        Task<int> DefineAccountRating(AccountDto account, CancellationToken token);
    }
}