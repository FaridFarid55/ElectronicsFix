using System.ComponentModel.DataAnnotations.Schema;

namespace Domains;

public partial class Customer
{
    [Key]
    public int CustomerId { get; set; }

    [Required(ErrorMessage = "Please Enter First Name")]
    [Display(Name = "First Name")]
    [StringLength(100)]
    public string FirstName { get; set; } = null!;

    [Required]
    [Display(Name = "Last Name")]
    [StringLength(100)]
    public string LastName { get; set; } = null!;

    [Required(ErrorMessage = "Please Enter Phone")]
    // [Range(0, int.MaxValue, ErrorMessage = "Phone price must be greater than zero.")]
    [StringLength(15, ErrorMessage = "Phone Length 15")]
    public string? Phone { get; set; }

    [StringLength(255)]
    public string? Address { get; set; }


    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string Email { get; set; } = null!;

    [Required]
    [StringLength(100)]

    [NotMapped]
    public string Password { get; set; } = null!;

    [NotMapped]
    [Required]
    [Display(Name = "Confirm Password")]
    [StringLength(100)]
    [Compare("Password", ErrorMessage = "Not Confirm")]
    public string ConfirmPassword { get; set; } = null!;

    [NotMapped]
    public string? ReturnUrl { get; set; }

    public int? FreeConsultationCount { get; set; }

    public virtual ICollection<Consultation> Consultations { get; set; } = new List<Consultation>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();


    public void UpdateCustomerInfo(Customer updatedCustomer)
    {

        FirstName = updatedCustomer.FirstName;
        LastName = updatedCustomer.LastName;
        Phone = updatedCustomer.Phone;
        Address = updatedCustomer.Address;

        // نترك الـ Email و EngineerId بدون تعديل
    }
}
