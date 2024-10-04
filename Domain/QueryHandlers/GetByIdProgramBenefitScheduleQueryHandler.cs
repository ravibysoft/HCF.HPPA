using HCF.HPPA.Common.Models;
using HCF.HPPA.Domain.Queries;
using HCF.HPPA.Domain.Services;
using MediatR;

namespace HCF.HPPA.Domain.QueryHandlers
{
    public class GetByIdProgramBenefitScheduleQueryHandler : IRequestHandler<GetByIdProgramBenefitScheduleQuery, ProgramBenefitSchedule>
    {
        private readonly IProgramBenefitScheduleService _service;
        public GetByIdProgramBenefitScheduleQueryHandler(IProgramBenefitScheduleService service)
        {
            _service = service;

        }
        public async Task<ProgramBenefitSchedule> Handle(GetByIdProgramBenefitScheduleQuery request, CancellationToken cancellationToken)
        {
            var response = await _service.GetByIdAsync(request.Id);

            return response;
        }
    }
}
