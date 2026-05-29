using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vamino.Domain.LoanGuarantorAgg.Entities;

namespace Vamino.Infrastructure.EfCore.EntityConfigs;

public class LoanGuarantorConfigs : IEntityTypeConfiguration<LoanGuarantor>
{
    public void Configure(EntityTypeBuilder<LoanGuarantor> builder)
    {
        builder.ToTable("LoanGuarantors");

        builder.HasKey(lg => lg.Id);

        builder.HasOne(lg => lg.LoanContract)
            .WithMany(lc => lc.LoanGuarantors)
            .HasForeignKey(lg => lg.LoanContractId)
            .OnDelete(DeleteBehavior.Cascade); 

        builder.HasOne(lg => lg.User)
            .WithMany(u => u.GuaranteedContracts)
            .HasForeignKey(lg => lg.UserId)
            .OnDelete(DeleteBehavior.Restrict); 

        builder.Property(lg => lg.GuarantorStatus)
            .IsRequired();

        builder.Property(lg => lg.Note)
            .HasMaxLength(500);

        builder.HasIndex(lg => new { lg.LoanContractId, lg.UserId }).IsUnique();
    }
}