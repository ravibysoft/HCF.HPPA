using HCF.HPPA.Common.Models;
using HCF.HPPA.Domain.Queries;
using HCF.HPPA.Domain.Services;
using MediatR;

namespace HCF.HPPA.Domain.QueryHandlers
{
    public class GetAllProgramBenefitScheduleQueryHandler : IRequestHandler<GetAllProgramBenefitScheduleQuery, IEnumerable<ProgramBenefitSchedule>>
    {
        private readonly IProgramBenefitScheduleService _service;
        public GetAllProgramBenefitScheduleQueryHandler(IProgramBenefitScheduleService service)
        {
            _service = service;

        }
        public async Task<IEnumerable<ProgramBenefitSchedule>> Handle(GetAllProgramBenefitScheduleQuery request, CancellationToken cancellationToken)
        {
            var response = await _service.GetAllAsync();

            return response;
        }
    }
}
