using HCF.HPPA.Domain.Commands;
using HCF.HPPA.Domain.Services;
using MediatR;

namespace HCF.HPPA.Domain.CommandHandlers
{
    public class DeleteProgramBenefitScheduleCommandHandler : IRequestHandler<DeleteProgramBenefitScheduleCommand, bool>
    {
        private readonly IProgramBenefitScheduleService _service;
        public DeleteProgramBenefitScheduleCommandHandler(IProgramBenefitScheduleService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(DeleteProgramBenefitScheduleCommand command, CancellationToken cancellationToken)
        {
            var response = await _service.DeleteAsync(command.Id);

            return response;
        }
    }
}
