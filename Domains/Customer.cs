namespace Domains;

public partial class Customer
{

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
    [StringLength(15)]
    public string? Phone { get; set; }

    [StringLength(255)]
    public string? Address { get; set; }


    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string Email { get; set; } = null!;

    [Required]
    [StringLength(100)]

    public string Password { get; set; } = null!;


    [Required]
    [Display(Name = "Confirm Password")]
    [StringLength(100)]
    public string ConfirmPassword { get; set; } = null!;

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
