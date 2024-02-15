using AppHouse.SharedKernel.DTOs;
using AppHouse.SharedKernel.Interfaces;

namespace AppHouse.Accounts.Core.Interfaces
{
    public interface IAccountActivityHistoryService : IBaseService<AccountActivityHistoryDto, Guid>
    {
    }
}
