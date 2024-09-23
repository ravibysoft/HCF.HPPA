using Entities.Models;

namespace Service.Interface;

public interface IProgramBenefitScheduleService
{
    Task<IEnumerable<ProgramBenefitSchedule>> GetAllAsync();
    Task<ProgramBenefitSchedule> GetByProgramCodeAndMBSItemCodeAsync(string programCode, string mbsItemCode);
    Task<ProgramBenefitSchedule> AddAsync(ProgramBenefitSchedule schedule);
    Task<ProgramBenefitSchedule> UpdateAsync(string programCode, string mbsItemCode, ProgramBenefitSchedule schedule);
    Task<bool> DeleteAsync(string programCode, string mbsItemCode);
}