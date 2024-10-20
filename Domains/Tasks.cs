namespace Domains;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;  
    public partial class Tasks
    {
        public int TaskId { get; set; }

        [Display(Name = "Engineer")]
        [Required(ErrorMessage = "Engineer is required.")]
        public int EngineerId { get; set; }

        [Display(Name = "Customer")]
        [Required(ErrorMessage = "Customer is required.")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Task title is required.")]
        [Display(Name = "Task Title")]
        [StringLength(200, ErrorMessage = "Task title cannot exceed 200 characters.")]
        public string TaskTitle { get; set; } = null!;

        [StringLength(1000, ErrorMessage = "Task description cannot exceed 1000 characters.")]
        [Display(Name = "Task Description")]
        public string? TaskDescription { get; set; }

        [Required(ErrorMessage = "Start date is required.")]
        [Display(Name = "Start Date")]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        [ValidateNever]
        public string Status { get; set; } = "Pending"; 

        [Range(0, 100000, ErrorMessage = "Task cost must be between 0 and 100,000.")]
        [DataType(DataType.Currency)]
        [Display(Name = "Task Cost")]
        public decimal TaskCost { get; set; }

        [ValidateNever]
        public virtual Customer Customer { get; set; } = null!;

        [ValidateNever]
        public virtual Engineer Engineer { get; set; } = null!;
    }

