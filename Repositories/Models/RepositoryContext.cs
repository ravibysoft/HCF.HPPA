using HCF.HPPA.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace HCF.HPPA.Repository.Models;

public class RepositoryContext : DbContext
{
    public RepositoryContext(DbContextOptions options)
        : base(options)
    {
    }
    public DbSet<ProgramBenefitSchedule> programBenefitSchedule { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProgramBenefitSchedule>().HasIndex(u => new { u.ProgramCode, u.MBSItemCode, u.DateOn }).IsUnique(true);
    }
}
