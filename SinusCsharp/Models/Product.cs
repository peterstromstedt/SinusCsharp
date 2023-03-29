using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SinusCsharp.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string ImageURL { get; set; }
        public string Color { get; set; }
        [Column(TypeName = "Money")]
        public decimal Price { get; set; }
        public string Description { get; set; }

        // setting default values here aswell
        public bool IsAvailable { get; set; } = true;
        public int Stock { get; set; } = 50;
        public List<OrderDetail>? Details { get; set; }        
    }
}
