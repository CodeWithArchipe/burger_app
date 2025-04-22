namespace CUT_Burger.Models;

public class Cart
{
    public List<BurgerCartItem> Items { get; set; } = new List<BurgerCartItem>();

    public decimal TotalPrice => Items.Sum(item => item.Price * item.Quantity);
}