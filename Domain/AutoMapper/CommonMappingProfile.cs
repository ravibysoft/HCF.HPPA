using AutoMapper;
using HCF.HPPA.Common.Models;
using HCF.HPPA.Domain.Commands;

namespace HCF.HPPA.Domain.AutoMapper
{
    public class CommonMappingProfile : Profile
    {
        public CommonMappingProfile()
        {
            CreateMap<ProgramBenefitSchedule, CreateProgramBenefitScheduleCommand>().ReverseMap();
            CreateMap<ProgramBenefitSchedule, UpdateProgramBenefitScheduleCommand>().ReverseMap();
        }
    }

}
