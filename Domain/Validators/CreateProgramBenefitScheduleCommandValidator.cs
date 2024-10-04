using FluentValidation;
using HCF.HPPA.Domain.Commands;

namespace HCF.HPPA.Domain.Validators
{
    public class CreateProgramBenefitScheduleCommandValidator : AbstractValidator<CreateProgramBenefitScheduleCommand>
    {
        public CreateProgramBenefitScheduleCommandValidator()
        {
            RuleFor(x => x.ProgramCode)
               .NotEmpty()
               .NotNull();
            RuleFor(x => x.MBSItemCode).NotEmpty().NotNull();
            RuleFor(x => x.MBSScheduleFees).NotEmpty().NotNull();
            RuleFor(x => x.ProgramMedicalFees).NotEmpty().NotNull();
        }
    }
}
