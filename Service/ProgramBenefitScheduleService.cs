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

    public Task<ProgramBenefitSchedule> GetByIdAsync(Int64 id) => _repository.GetByIdAsync(id);

    public Task<ProgramBenefitSchedule> AddAsync(ProgramBenefitSchedule schedule) => _repository.AddAsync(schedule);

    public Task<ProgramBenefitSchedule> UpdateAsync(ProgramBenefitSchedule schedule) => _repository.UpdateAsync(schedule);

    public Task<bool> DeleteAsync(Int64 id) => _repository.DeleteAsync(id);
}