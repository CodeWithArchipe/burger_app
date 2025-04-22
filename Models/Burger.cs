using System.ComponentModel.DataAnnotations;

namespace CUT_Burger.Models;

public class Burger
{

    [Key]
    public int Id { get; set; } // Unique identifier for the item
    public string Name { get; set; } // Name of the item
    public decimal Price { get; set; } // Price of the item
    public string Description { get; set; } // Description of the item
    public bool Availability { get; set; } // Whether the item is available or not

}    