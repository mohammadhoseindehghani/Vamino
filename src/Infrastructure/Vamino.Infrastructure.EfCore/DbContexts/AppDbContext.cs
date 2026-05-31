using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using Vamino.Domain.LoanContractAgg.Entities;
using Vamino.Domain.LoanGuarantorAgg.Entities;
using Vamino.Domain.UserAgg.Entities;

namespace Vamino.Infrastructure.EfCore.DbContexts;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<LoanContract> LoanContracts { get; set; }
    public DbSet<LoanGuarantor> LoanGuarantors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}