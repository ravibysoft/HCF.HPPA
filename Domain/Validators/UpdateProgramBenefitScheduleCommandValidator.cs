using FluentValidation;
using HCF.HPPA.Domain.Commands;

namespace HCF.HPPA.Domain.Validators
{
    public class UpdateProgramBenefitScheduleCommandValidator : AbstractValidator<UpdateProgramBenefitScheduleCommand>
    {
        public UpdateProgramBenefitScheduleCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();
            RuleFor(x => x.ProgramCode)
               .NotEmpty()
               .NotNull();
            RuleFor(x => x.MBSItemCode).NotEmpty().NotNull();
            RuleFor(x => x.MBSScheduleFees).NotEmpty().NotNull();
            RuleFor(x => x.ProgramMedicalFees).NotEmpty().NotNull();
        }
    }
}
