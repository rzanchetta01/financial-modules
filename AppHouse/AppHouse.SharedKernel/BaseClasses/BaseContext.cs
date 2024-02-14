using AppHouse.SharedKernel.BaseClasses;
using AppHouse.SharedKernel.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AppHouse.SharedKernel.BaseClasses;

public abstract class BaseContext(DbContextOptions options) : DbContext (options)
{
    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        ChangeTracker
            .Entries()
            .Where(entry => entry.State == EntityState.Deleted && entry.Entity is BaseEntity)
            .ToList()
            .ForEach(entry => 
            {
                entry.State = EntityState.Modified;
                entry.CurrentValues["IsActive"] = false;
            });

        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("account_pkey");

            entity.ToTable("account");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.AddressComplement).HasColumnName("address_complement");
            entity.Property(e => e.BirthDate).HasColumnName("birth_date");
            entity.Property(e => e.Cellphone)
                .HasMaxLength(20)
                .HasColumnName("cellphone");
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .HasColumnName("city");
            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .HasColumnName("country");
            entity.Property(e => e.CreditScore).HasColumnName("credit_score");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("date_created");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Income)
                .HasPrecision(18, 2)
                .HasColumnName("income");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.PostalCode)
                .HasMaxLength(20)
                .HasColumnName("postal_code");
            entity.Property(e => e.State)
                .HasMaxLength(100)
                .HasColumnName("state");
        });

        modelBuilder.Entity<AccountActivityHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("account_activity_history_pkey");

            entity.ToTable("account_activity_history");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("date_created");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.LoanId).HasColumnName("loan_id");
            entity.Property(e => e.IsReceiver).HasColumnName("is_receiver");

            entity.HasOne(d => d.Account).WithMany(p => p.AccountActivityHistories)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("account_activity_history_account_id_fkey");

            entity.HasOne(d => d.Loan).WithMany(p => p.AccountActivityHistories)
                .HasForeignKey(d => d.LoanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("account_activity_history_loan_id_fkey");
        });

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

            entity.HasOne(d => d.CreatorAccount).WithMany(p => p.Loans)
                .HasForeignKey(d => d.CreatorAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("loan_creator_account_id_fkey");

        });

        HandleOnlyActiveEntities(ref modelBuilder);
    }

    private static void HandleOnlyActiveEntities(ref ModelBuilder modelBuilder)
    {
        var entityTypes = modelBuilder.Model.GetEntityTypes()
            .Where(e => typeof(BaseEntity).IsAssignableFrom(e.ClrType));

        foreach (var entityType in entityTypes)
        {
            modelBuilder.Entity(entityType.ClrType)
                .HasQueryFilter(RetrieveOnlyActives(entityType.ClrType));
        }
    }

    private static LambdaExpression RetrieveOnlyActives(Type type)
    {
        var parameter = Expression.Parameter(type, "entity");
        var property = Expression.Property(parameter, "IsActive");
        var notDeleted = Expression.Constant(true);
        var condition = Expression.Equal(property, notDeleted);
        return Expression.Lambda(condition, parameter);
    }

}
