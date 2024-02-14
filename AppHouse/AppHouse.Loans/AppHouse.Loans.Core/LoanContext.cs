using AppHouse.SharedKernel.BaseClasses;
using AppHouse.SharedKernel.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppHouse.Loans.Core
{
    public class LoanContext(DbContextOptions options) : BaseContext(options)
    {
        public virtual DbSet<Loan> Loans { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
#if DEBUG
            optionsBuilder.EnableDetailedErrors(true);
#endif
        }
    }
}
