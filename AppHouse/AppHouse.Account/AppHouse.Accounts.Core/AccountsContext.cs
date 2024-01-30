using AppHouse.Accounts.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace AppHouse.Accounts.Core
{
    internal class AccountsContext : DbContext
    {
        public virtual DbSet<Account> Accounts { get; set; }
    }
}
