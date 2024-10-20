using System.ComponentModel.DataAnnotations;

namespace Domains
{
    public class Engineer
    {
        public int EngineerId { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(50, ErrorMessage = "First Name cannot exceed 50 characters.")]
        public string FirstName { get; set; }=string.Empty;

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(50, ErrorMessage = "Last Name cannot exceed 50 characters.")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone is required.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(100, ErrorMessage = "Address cannot exceed 100 characters.")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; } = string.Empty;// يجب أن يبقى غير قابل للتعديل
        public string Password { get; set; } = string.Empty; // للتخزين الآمن يجب استخدام hashing
        public string ConfirmPassword { get; set; } = string.Empty;
        public virtual ICollection<Consultation> Consultations { get; set; } = new List<Consultation>();
        // ميثود لتحديث المعلومات
        public void UpdateEngineerInfo(Engineer updatedEngineer)
        {
            
            FirstName = updatedEngineer.FirstName;
            LastName = updatedEngineer.LastName;
            Phone = updatedEngineer.Phone;
            Address = updatedEngineer.Address;

            // نترك الـ Email و EngineerId بدون تعديل
        }
    }
}
