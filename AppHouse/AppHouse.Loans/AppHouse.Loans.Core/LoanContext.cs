using AppHouse.Loans.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppHouse.Loans.Core
{
    public class LoanContext(DbContextOptions options) : DbContext(options)
    {
        public virtual DbSet<Loan> Loans { get; set; }
    }
}
