using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vamino.Domain.UserAgg.Entities;

namespace Vamino.Infrastructure.EfCore.EntityConfigs;

public class UserConfigs : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.NationalCode)
            .IsRequired()
            .HasMaxLength(10); 

        builder.Property(u => u.PhoneNumber)
            .IsRequired()
            .HasMaxLength(11);

        builder.HasIndex(u => u.NationalCode).IsUnique();
        builder.HasIndex(u => u.PhoneNumber).IsUnique();
    }
}