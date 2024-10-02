using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository;

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
