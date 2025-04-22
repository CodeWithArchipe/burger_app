using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CUT_Burger.Models;

public class Order
{
    [Key]
    public int OrderId { get; set; }

    [Required]
    public string OrderNumber { get; set; } = string.Empty; // Default to an empty string

    [Required]
    public decimal TotalAmount { get; set; }

    [Required] // Make OrderStatus required
    public string OrderStatus { get; set; } = string.Empty; // Default to an empty string

    [Required] // Make EstimatedDeliveryTime required
    public string EstimatedDeliveryTime { get; set; } = string.Empty; // Default to an empty string

    // Foreign key for User
    public int UserId { get; set; }

    [ForeignKey("UserId")]
    public User User { get; set; } // Navigation property

    // Other properties for shipping info and payment method
    [Required] // Make PaymentMethod required
    public string PaymentMethod { get; set; } = string.Empty; // Default to an empty string

    [Required] // Make UserName required
    public string UserName { get; set; } = string.Empty; // Default to an empty string

    [Required] // Make UserAddress required
    public string UserAddress { get; set; } = string.Empty; // Default to an empty string

    [Required] // Make UserCity required
    public string UserCity { get; set; } = string.Empty; // Default to an empty string

    public string UserState { get; set; } = string.Empty; // Default to an empty string
    public string UserZip { get; set; } = string.Empty; // Default to an empty string
}