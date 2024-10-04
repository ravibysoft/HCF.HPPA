using MediatR;

namespace HCF.HPPA.Domain.Commands
{
    public class DeleteProgramBenefitScheduleCommand : IRequest<bool>
    {
        public long Id { get; set; }

    }
}
