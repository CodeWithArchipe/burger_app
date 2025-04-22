// namespace CUT_Burger.Models;
//
// public class DeliveryPerson : User
// {
//    
//         public string Status { get; set; } // Status of the delivery person (e.g., "Available", "On Delivery")
//
//         public void UpdateOrderStatus(int orderId, string newStatus)
//         {
//             // Check for valid status
//             if (string.IsNullOrEmpty(newStatus))
//             {
//                 throw new ArgumentException("Status cannot be null or empty.");
//             }
//
//             // Update the status of the order
//             using (var context = new AppDbContext())
//             {
//                 var order = context.Orders.Find(orderId);
//                 if (order == null)
//                 {
//                     throw new ArgumentException("Order not found.");
//                 }
//
//                 // Update the order status
//                 order.Status = newStatus;
//
//                 // Save changes to the database
//                 context.SaveChanges();
//             }
//         }
// }
