using AppHouse.SharedKernel.Interfaces;

namespace AppHouse.Accounts.Core.Interfaces
{
    public interface IAccountRepository : IBaseRepository<Domain.Entity.Account, Guid>
    {
    }
}
