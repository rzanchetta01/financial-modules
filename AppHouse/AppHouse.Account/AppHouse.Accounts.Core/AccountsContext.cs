using AppHouse.SharedKernel.BaseClasses;
using AppHouse.SharedKernel.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppHouse.Accounts.Core;

public class AccountsContext(DbContextOptions<AccountsContext> options) : BaseContext(options)
{
    public virtual DbSet<Account> Accounts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
#if DEBUG
        optionsBuilder.EnableDetailedErrors(true);
#endif
    }

}
