using System.ComponentModel.DataAnnotations;

namespace Entities.Models;

public class ProgramBenefitSchedule
{
    [Key]
    public Int64 Id { get; set; }
    public string ProgramCode { get; set; } = string.Empty;
    public string MBSItemCode { get; set; } = string.Empty;
    public decimal MBSScheduleFees { get; set; }
    public decimal ProgramMedicalFees { get; set; }
    public DateTime DateOn { get; set; }
    public DateTime? DateOff { get; set; }
    public string ChangedBy { get; set; } = string.Empty;
    public DateTime ChangedDateTime { get; set; }
    public string Comments { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}
