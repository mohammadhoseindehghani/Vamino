using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vamino.Domain.LoanContractAgg.Entities;

namespace Vamino.Infrastructure.EfCore.EntityConfigs;

public class LoanContractConfigs : IEntityTypeConfiguration<LoanContract>
{
    public void Configure(EntityTypeBuilder<LoanContract> builder)
    {
        builder.ToTable("LoanContracts");

        builder.HasKey(lc => lc.Id);

        builder.Property(lc => lc.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(lc => lc.Amount)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(lc => lc.Description)
            .HasMaxLength(1000);

        builder.Property(lc => lc.LoanStatus)
            .IsRequired();

        builder.Property(lc => lc.CreatedAt)
            .HasDefaultValueSql("GETDATE()");

        builder.HasOne(lc => lc.Borrower)
            .WithMany(u => u.BorrowerContracts)
            .HasForeignKey(lc => lc.BorrowerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}