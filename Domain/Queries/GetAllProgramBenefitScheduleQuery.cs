using HCF.HPPA.Common.Models;
using MediatR;

namespace HCF.HPPA.Domain.Queries
{
    public class GetAllProgramBenefitScheduleQuery : IRequest<IEnumerable<ProgramBenefitSchedule>>
    {
    }
}
