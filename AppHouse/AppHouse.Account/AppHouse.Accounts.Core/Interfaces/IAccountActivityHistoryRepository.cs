using AppHouse.SharedKernel.Entities;
using AppHouse.SharedKernel.Interfaces;

namespace AppHouse.Accounts.Core.Interfaces
{
    public interface IAccountActivityHistoryRepository : IBaseRepository<AccountActivityHistory, Guid>
    {
    }
}
