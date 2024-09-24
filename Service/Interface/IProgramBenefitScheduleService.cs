using Entities.Models;

namespace Service.Interface;

public interface IProgramBenefitScheduleService
{
    Task<IEnumerable<ProgramBenefitSchedule>> GetAllAsync();
    Task<ProgramBenefitSchedule> GetByIdAsync(Int64 id);
    Task<ProgramBenefitSchedule> AddAsync(ProgramBenefitSchedule schedule);
    Task<ProgramBenefitSchedule> UpdateAsync(ProgramBenefitSchedule schedule);
    Task<bool> DeleteAsync(Int64 id);
}