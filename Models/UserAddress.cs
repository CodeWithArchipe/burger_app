using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CUT_Burger.Models
{
    public class UserAddress
    {
        [Key]
        [Required]
        public int UserAddressId { get; set; }

        [Required] // If address should always be provided
        public string Address { get; set; }

        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }

        // Foreign Key relationship
        public int UserId { get; set; } // Foreign key field

        [ForeignKey(nameof(UserId))] // Declares the UserId as the foreign key
        public User? User { get; set; } // Navigation property for the associated user
    }
}