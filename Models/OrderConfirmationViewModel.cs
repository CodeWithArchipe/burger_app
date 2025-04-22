namespace CUT_Burger.Models;

public class OrderConfirmationViewModel
{
    public string OrderNumber { get; set; } = string.Empty;
    public string EstimatedDeliveryTime { get; set; } = string.Empty;
    public string PaymentMethod { get; set; } = string.Empty;
    public List<BurgerCartItem> Items { get; set; } = new(); // List of ordered items
    public decimal TotalAmount { get; set; } // Total amount of the order
}