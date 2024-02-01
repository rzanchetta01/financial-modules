using AppHouse.Accounts.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace AppHouse.Accounts.Core
{
    public class AccountsContext(DbContextOptions<AccountsContext> options) : DbContext(options)
    {
        public virtual DbSet<Account> Accounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(e =>
            {
                e.ToTable("account");
            });

            //base.OnModelCreating(modelBuilder);
        }
    }
}
