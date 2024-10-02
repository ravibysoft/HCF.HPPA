using Entities.Models;
using HCF.HPPA.Entities.Models;

namespace Repository.Interface;

public interface IProgramBenefitScheduleRepository
{
    Task<IEnumerable<ProgramBenefitSchedule>> GetAllAsync();
    Task<ProgramBenefitSchedule> GetByIdAsync(Int64 id);
    Task<ProgramBenefitSchedule> AddAsync(ProgramBenefitSchedule schedule);
    void UpdateAsync(ProgramBenefitSchedule schedule);
    Task<bool> DeleteAsync(Int64 id);

    Task<PagedResult<ProgramBenefitSchedule>> GetPagedAsync(
        string? search = null,
        string? sortBy = null,
        bool ascending = true,
        int pageNumber = 1,
        int pageSize = 10);
}
