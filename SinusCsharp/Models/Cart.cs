using Microsoft.EntityFrameworkCore;

namespace SinusCsharp.Models
{
    [Index(nameof(ProductId), IsUnique = true)]
    public class Cart
    {
        public int CartId { get; set; }        
        public int ProductId { get; set; }
        public int Quantity { get; set; } = 1;
    }
}
