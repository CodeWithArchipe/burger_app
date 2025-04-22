namespace CUT_Burger.Models
{
    public class MenuBurger : Burger
    {
        public MenuBurger(int itemId, string itemName, decimal price, string description, bool availability)
        {
            Id = itemId;
            Name = itemName;
            Price = price;
            Description = description;
            Availability = availability;
        }

        // Properties (Ensure these properties are defined in the Burger class)
        public int Id { get; set; }              // Assuming Id is in Burger
        public string Name { get; set; }          // Assuming Name is in Burger
        public decimal Price { get; set; }        // Assuming Price is in Burger
        public string Description { get; set; }   // Assuming Description is in Burger
        public bool Availability { get; set; }     // Assuming Availability is in Burger

        // Methods
        public void ToggleAvailability()
        {
            Availability = !Availability;
        }

        public void UpdatePrice(decimal newPrice)
        {
            Price = newPrice;
        }

        public void UpdateDescription(string newDescription)
        {
            Description = newDescription;
        }

        public override string ToString()
        {
            return $"{Name}: {Description} - {Price:C} (Available: {Availability})";
        }
    }
}