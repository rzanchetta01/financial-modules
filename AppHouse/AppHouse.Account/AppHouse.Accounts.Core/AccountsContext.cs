using AppHouse.Accounts.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace AppHouse.Accounts.Core
{
    public class AccountsContext(DbContextOptions<AccountsContext> options) : DbContext(options)
    {
        public virtual DbSet<Account> Accounts { get; set; }
    }
}
