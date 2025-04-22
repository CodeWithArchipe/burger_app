using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CUT_Burger.Models;

public class CheckoutViewModel
{
    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; set; } 
        
    [Key]
    [Required(ErrorMessage = "User ID is required.")]
    public int UserId { get; set; }

    [Required(ErrorMessage = "Address is required.")]
    [StringLength(100, ErrorMessage = "Address cannot exceed 100 characters.")]
    public string Address { get; set; }

    [Required(ErrorMessage = "City is required.")]
    public string City { get; set; }

    [Required(ErrorMessage = "State is required.")]
    public string State { get; set; }

    [Required(ErrorMessage = "Zip code is required.")]
    // [RegularExpression(@"^\d{5}-\d{4}|\d{5}$", ErrorMessage = "Invalid Zip Code format.")]
    public string Zip { get; set; }

    // [Required(ErrorMessage = "Payment method is required.")]
    public string PaymentMethod { get; set; }

    public List<BurgerCartItem> Items { get; set; } = new();
}