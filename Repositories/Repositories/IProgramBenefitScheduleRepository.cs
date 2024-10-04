using HCF.HPPA.Common.Models;

namespace HCF.HPPA.Repository.Repositories;

public interface IProgramBenefitScheduleRepository
{
    Task<IEnumerable<ProgramBenefitSchedule>> GetAllAsync();
    Task<ProgramBenefitSchedule> GetByIdAsync(long id);
    Task<ProgramBenefitSchedule> AddAsync(ProgramBenefitSchedule schedule);
    Task<bool> UpdateAsync(ProgramBenefitSchedule schedule);
    Task<bool> DeleteAsync(long id);

    Task<PagedResult<ProgramBenefitSchedule>> GetPagedAsync(
        string? search = null,
        string? sortBy = null,
        bool ascending = true,
        int pageNumber = 1,
        int pageSize = 10);
}
