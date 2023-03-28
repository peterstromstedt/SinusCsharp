using System.ComponentModel.DataAnnotations;

namespace SinusCsharp.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        public string Anna { get; set; }

        // setting default values here aswell
        public bool IsAvailable { get; set; } = true;
        public int Stock { get; set; } = 50;
        public List<OrderDetail>? Details { get; set; }        
    }
}
