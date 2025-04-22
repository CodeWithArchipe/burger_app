public class BurgerCartItem
{
    public int Id { get; set; }  // Identifier for the burger
    public string Name { get; set; }  // Name of the burger
    public decimal Price { get; set; }  // Price of the burger
    public int Quantity { get; set; }  // Quantity of this burger in the cart
    public class Order
    {
        public string OrderNumber { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentMethod { get; set; }
        public string EstimatedDeliveryTime { get; set; }
        public string OrderStatus { get; set; }
        public string UserId { get; set; }  // Ensure this is string if coming from Claims
        // If there are any integer properties, ensure they're assigned correctly
    }
}