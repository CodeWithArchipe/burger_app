namespace CUT_Burger.Models;

public class Menu
{
    // Attributes
    public List<MenuBurger> Items { get; set; } = new List<MenuBurger>(); // List of menu items

    // Method to add a new item to the menu
    public void AddItem(MenuBurger burger)
    {
        if (burger == null)
        {
            throw new ArgumentNullException(nameof(burger), "Item cannot be null.");
        }

        // Check if the item already exists in the menu
        if (Items.Any(i => i.Id == burger.Id))
        {
            throw new ArgumentException("Item with the same ID already exists.");
        }

        Items.Add(burger);
    }

    // Method to remove an item from the menu by its ID
    public void RemoveItem(int Id)
    {
        var item = Items.FirstOrDefault(i => i.Id == Id);
        if (item == null)
        {
            throw new ArgumentException("Item not found.");
        }

        Items.Remove(item);
    }

    // Method to update an existing menu item
    public void UpdateItem(MenuBurger updatedBurger)
    {
        if (updatedBurger == null)
        {
            throw new ArgumentNullException(nameof(updatedBurger), "Item cannot be null.");
        }

        var existingItem = Items.FirstOrDefault(i => i.Id == updatedBurger.Id);
        if (existingItem == null)
        {
            throw new ArgumentException("Item not found.");
        }

        // Update the properties of the existing item
        existingItem.Name = updatedBurger.Name;
        existingItem.Price = updatedBurger.Price;
        existingItem.Description = updatedBurger.Description;
        existingItem.Availability = updatedBurger.Availability;
    }

    // Method to retrieve a menu item by its ID
    public MenuBurger GetItem(int Id)
    {
        return Items.FirstOrDefault(i => i.Id == Id);
    }

    // Method to display all items in the menu
    public void DisplayMenu()
    {
        //to be implemented
    }
}