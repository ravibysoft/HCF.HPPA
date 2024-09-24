using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;

namespace Repository;

public class ProgramBenefitScheduleRepository : IProgramBenefitScheduleRepository
{
    private readonly RepositoryContext _context;

    public ProgramBenefitScheduleRepository(RepositoryContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProgramBenefitSchedule>> GetAllAsync()
    {
        return await _context.programBenefitSchedule.ToListAsync();
    }

    public async Task<ProgramBenefitSchedule> GetByIdAsync(Int64 id)
    {
        return await _context.programBenefitSchedule.FirstAsync(x => x.Id == id);
    }

    public async Task<ProgramBenefitSchedule> AddAsync(ProgramBenefitSchedule schedule)
    {
        _context.programBenefitSchedule.Add(schedule);
        await _context.SaveChangesAsync();
        return schedule;
    }

    public async Task<ProgramBenefitSchedule> UpdateAsync(ProgramBenefitSchedule schedule)
    {
        schedule.ChangedDateTime = DateTime.Now;
        _context.Entry(schedule).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return schedule;
    }

    public async Task<bool> DeleteAsync(Int64 id)
    {
        var schedule = await _context.programBenefitSchedule.FirstOrDefaultAsync(x => x.Id == id);

        if (schedule == null) return false;

        _context.programBenefitSchedule.Remove(schedule);
        await _context.SaveChangesAsync();
        return true;
    }
}
