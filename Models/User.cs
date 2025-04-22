using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CUT_Burger.Models;

public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string? Username { get; set; } = string.Empty;

    [Required]
    [EmailAddress] // verifyy later
    public string? Email { get; set; } // Email can be nullable if necessary

    [Required]
    public string Password { get; set; }

    public string? City { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }

    // This field will be used for password confirmation and won't be mapped to the database
    [NotMapped]
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    public string? ConfirmPassword { get; set; }

    // Navigation property for the user's address
    public UserAddress? Address { get; set; }

    [Required]
    public bool IsAdmin { get; set; }
    // Navigation property for the orders placed by the user
    public ICollection<Order> Orders { get; set; } = new List<Order>(); // Initialize to avoid null reference


    // Parameterized constructor for more control over object initialization
    public User(int id, string username, string email, string password, UserAddress? address)
    {
        Id = id;
        Username = username;
        Email = email;
        Password = password;
        Address = address;
        IsAdmin = false; // Default to non-admin unless specified
    }

    // Default constructor is needed for EF Core to instantiate the model
    public User()
    {
    }
}