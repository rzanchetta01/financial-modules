using AppHouse.SharedKernel.Core.BaseClasses;
using AppHouse.SharedKernel.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppHouse.Loans.Core
{
    public class LoanContext(DbContextOptions options) : BaseContext(options)
    {
        public virtual DbSet<Loan> Loans { get; set; }
    }
}
