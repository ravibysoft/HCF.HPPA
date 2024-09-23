using Entities.Models;
using Repository.Interface;
using Service.Interface;

namespace Service;

public class ProgramBenefitScheduleService : IProgramBenefitScheduleService
{
    private readonly IProgramBenefitScheduleRepository _repository;

    public ProgramBenefitScheduleService(IProgramBenefitScheduleRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<ProgramBenefitSchedule>> GetAllAsync() => _repository.GetAllAsync();

    public Task<ProgramBenefitSchedule> GetByProgramCodeAndMBSItemCodeAsync(string programCode, string mbsItemCode) => _repository.GetByProgramCodeAndMBSItemCodeAsync(programCode,mbsItemCode);

    public Task<ProgramBenefitSchedule> AddAsync(ProgramBenefitSchedule schedule) => _repository.AddAsync(schedule);

    public Task<ProgramBenefitSchedule> UpdateAsync(string programCode, string mbsItemCode, ProgramBenefitSchedule schedule) => _repository.UpdateAsync(programCode,mbsItemCode, schedule);

    public Task<bool> DeleteAsync(string programCode, string mbsItemCode) => _repository.DeleteAsync(programCode, mbsItemCode);
}