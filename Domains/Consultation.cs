namespace Domains;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

public partial class Consultation
{
    [Key]
    public int ConsultationId { get; set; }

    [Display(Name = "Engineer")]
    [Required(ErrorMessage = "Engineer is required.")]
    public int EngineerId { get; set; }

    [Display(Name = "Customer")]
    [Required(ErrorMessage = "Customer is required.")]
    public int CustomerId { get; set; }

    [Required(ErrorMessage = "Start date is required.")]
    [Display(Name = "Start Date")]
    [DataType(DataType.DateTime)]
    public DateTime StartDate { get; set; }

    [DataType(DataType.DateTime)]
    [Display(Name = "End Date")]
    public DateTime? EndDate { get; set; }

    [ValidateNever]
    public string Status { get; set; } = null!;

    [StringLength(1000, ErrorMessage = "Issue description cannot exceed 1000 characters.")]
    [Display(Name = "Issue Description")]
    public string? IssueDescription { get; set; }

    [Range(0, 100000, ErrorMessage = "Consultation cost must be between 0 and 100,000.")]
    [DataType(DataType.Currency)]
    [Display(Name = "Consultation Cost")]
    public decimal ConsultationCost { get; set; }

    [ValidateNever]
    public virtual Customer Customer { get; set; } = null!;
    [ValidateNever]
    public virtual Engineer Engineer { get; set; } = null!;
}
