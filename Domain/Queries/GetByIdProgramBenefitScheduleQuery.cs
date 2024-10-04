using HCF.HPPA.Common.Models;
using MediatR;

namespace HCF.HPPA.Domain.Queries
{
    public class GetByIdProgramBenefitScheduleQuery : IRequest<ProgramBenefitSchedule>
    {
        public long Id { get; set; }
    }
}
