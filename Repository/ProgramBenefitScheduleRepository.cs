using Entities.Models;
using HCF.HPPA.Entities.Models;
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

    public async void UpdateAsync(ProgramBenefitSchedule schedule)
    {
        schedule.ChangedDateTime = DateTime.Now;
        _context.Entry(schedule).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(Int64 id)
    {
        var schedule = await _context.programBenefitSchedule.FirstOrDefaultAsync(x => x.Id == id);

        if (schedule == null) return false;

        _context.programBenefitSchedule.Remove(schedule);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<PagedResult<ProgramBenefitSchedule>> GetPagedAsync(
       string? search = null,
       string? sortBy = null,
       bool ascending = true,
       int pageNumber = 1,
       int pageSize = 10)
    {
        var query = _context.programBenefitSchedule.AsQueryable();

        // Search
        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(p => p.ProgramCode.Contains(search) ||
                                      p.MBSItemCode.Contains(search) ||
                                      p.Comments.Contains(search));
        }

        // Sort
        if (!string.IsNullOrWhiteSpace(sortBy))
        {
            query = ascending ?
                query.OrderBy(p => EF.Property<object>(p, sortBy)) :
                query.OrderByDescending(p => EF.Property<object>(p, sortBy));
        }

        // Pagination
        var totalRecords = await query.CountAsync();
        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<ProgramBenefitSchedule>
        {
            TotalRecords = totalRecords,
            PageNumber = pageNumber,
            PageSize = pageSize,
            Items = items
        };
    }
}
