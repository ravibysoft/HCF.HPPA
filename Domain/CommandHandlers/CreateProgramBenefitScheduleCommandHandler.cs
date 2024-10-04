using AutoMapper;
using HCF.HPPA.Common.Models;
using HCF.HPPA.Domain.Commands;
using HCF.HPPA.Domain.Services;
using MediatR;

namespace HCF.HPPA.Domain.CommandHandlers
{
    public class CreateProgramBenefitScheduleCommandHandler : IRequestHandler<CreateProgramBenefitScheduleCommand, ProgramBenefitSchedule>
    {
        private readonly IProgramBenefitScheduleService _service;
        private readonly IMapper _mapper;
        public CreateProgramBenefitScheduleCommandHandler(IProgramBenefitScheduleService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ProgramBenefitSchedule> Handle(CreateProgramBenefitScheduleCommand command, CancellationToken cancellationToken)
        {
            var programBenefitSchedule = _mapper.Map<ProgramBenefitSchedule>(command);
            var response = await _service.AddAsync(programBenefitSchedule);

            return response;
        }
    }
}
