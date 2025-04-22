using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CUT_Burger.Models;

public class RegisterViewModel
{
    [Microsoft.Build.Framework.Required]
    public string Username { get; set; }

    [Microsoft.Build.Framework.Required]
    // [EmailAddress]
    public string Email { get; set; }

    [Microsoft.Build.Framework.Required]
    public string Password { get; set; }

    [Microsoft.Build.Framework.Required]
    public string Address { get; set; }

    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    // This field will be used for password confirmation and won't be mapped to the database
    [NotMapped]
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    public string? ConfirmPassword { get; set; }
}
