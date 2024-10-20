

using System.ComponentModel.DataAnnotations;

namespace ElectronicsFix.Models
{
    public class UserModel : Login
    {
        [Required(ErrorMessage = "Please Enter First Name")]
        [MaxLength(200, ErrorMessage = "Please Enter  200 Max length")]
        public string FirstName { get; set; } = default!;

        [Required(ErrorMessage = "Please Enter Last Name")]
        [MaxLength(200, ErrorMessage = "Please Enter  200 Max length")]
        public string LastName { get; set; } = default!;
    }
}
