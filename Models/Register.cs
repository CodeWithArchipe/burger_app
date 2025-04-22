
using System.ComponentModel.DataAnnotations;

namespace CUT_Burger.Models
{
    public class Register
    {
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please confirm your password.")]
        [Compare("Password", ErrorMessage = "Password and Confirm Password must match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        // Optional fields
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }

        // Nested address validation
        [Required]
        public string Address { get; set; } // Make sure Address is a string if you want to assign it directly
    }
}