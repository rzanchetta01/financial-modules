using AppHouse.SharedKernel.DTOs;
using AppHouse.SharedKernel.Interfaces;

namespace AppHouse.Accounts.Core.Interfaces
{
    public interface IAccountService : IBaseService<AccountDto, Guid>
    {
        Task<int> DefineStartAccountRating(AccountDto account, CancellationToken token);
    }
}