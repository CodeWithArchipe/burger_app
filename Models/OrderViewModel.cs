
namespace CUT_Burger.Models
{
    public class OrderViewModel
    {
        public string OrderNumber { get; set; }
        public string PaymentMethod { get; set; }
        public List<BurgerCartItem> Items { get; set; }
        public decimal TotalAmount { get; set; }
        public string EstimatedDeliveryTime { get; set; }
        public string OrderStatus { get; set; } // e.g., "Pending", "Completed", etc.

        // User Information
        public string UserName { get; set; }
        public string UserAddress { get; set; }
        public string UserCity { get; set; }
        public string UserState { get; set; }
        public string UserZip { get; set; }
    }
}