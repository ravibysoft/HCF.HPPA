using Entities.Models;
using HCF.HPPA.Entities.Models;

namespace Service.Interface;

public interface IProgramBenefitScheduleService
{
    Task<IEnumerable<ProgramBenefitSchedule>> GetAllAsync();
    Task<ProgramBenefitSchedule> GetByIdAsync(Int64 id);
    Task<ProgramBenefitSchedule> AddAsync(ProgramBenefitSchedule schedule);
    Task<ProgramBenefitSchedule> UpdateAsync(ProgramBenefitSchedule schedule);
    Task<bool> DeleteAsync(Int64 id);
    Task<PagedResult<ProgramBenefitSchedule>> GetPagedSchedulesAsync(
       string? search = null,
       string? sortBy = null,
       bool ascending = true,
       int pageNumber = 1,
       int pageSize = 10);
}