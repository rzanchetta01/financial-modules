using AppHouse.SharedKernel.BaseClasses;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.HasDefaultSchema("loans");

            modelBuilder.Entity<Loan>(entity =>
            {
                
                entity.HasKey(e => e.Id).HasName("loan_pkey");

                entity.ToTable("loan");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");
                entity.Property(e => e.CreatorAccountId).HasColumnName("creator_account_id");
                entity.Property(e => e.DateCreated)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasColumnName("date_created");
                entity.Property(e => e.IsActive)
                    .HasDefaultValue(true)
                    .HasColumnName("is_active");
                entity.Property(e => e.LoanDescription).HasColumnName("loan_description");
                entity.Property(e => e.LoanQualityRating).HasColumnName("loan_quality_rating");
                entity.Property(e => e.LoanStyleType).HasColumnName("loan_style_type");
                entity.Property(e => e.MaxAmount).HasColumnName("max_amount");
                entity.Property(e => e.MaxDateFeasible).HasColumnName("max_date_feasible");
                entity.Property(e => e.MinAmount).HasColumnName("min_amount");
                entity.Property(e => e.MinDateFeasible).HasColumnName("min_date_feasible");

            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
