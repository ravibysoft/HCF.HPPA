using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class RepositoryContext : DbContext
{
	public RepositoryContext(DbContextOptions options)
		: base(options)
	{
	}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProgramBenefitSchedule>().Property(x => x.ProgramCode).IsConcurrencyToken();
    }
    public DbSet<ProgramBenefitSchedule> programBenefitSchedule { get; set; }
}
