
using System.ComponentModel.DataAnnotations;

namespace ElectronicsFix.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Please Enter Email")]
        [EmailAddress]
        public string Email { get; set; } = default!;


        [DataType(DataType.Password, ErrorMessage = "not Valid")]
        [Required(ErrorMessage = "Please Enter Password")]
        public string Password { get; set; } = default!;

        public string? ReturnUrl { get; set; }
    }
}