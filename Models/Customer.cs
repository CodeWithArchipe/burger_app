namespace CUT_Burger.Models
{
    public class Customer : User
    {
        // Properties
        public string DeliveryAddress { get; set; }
        public List<Order> OrderHistory { get; set; }

        // Constructor
        public Customer(int id, string username, string email, string password, string deliveryAddress, UserAddress? address)
            : base(id, username, email, password, address)
        {
            DeliveryAddress = deliveryAddress;
            OrderHistory = new List<Order>();
        }

        // Methods
        public void PlaceOrder(Order order)
        {
            // Logic to place a new order
            OrderHistory.Add(order);
        }

        public List<Order> ViewOrderHistory()
        {
            // Logic to return order history
            return OrderHistory;
        }

        public void UpdateProfile(string newEmail, string newPassword)
        {
            // Logic to update user profile
            Email = newEmail;  // Updated property name
            Password = newPassword;  // Updated property name
        }
    }
}