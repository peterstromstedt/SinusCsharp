using Microsoft.EntityFrameworkCore;

namespace SinusCsharp.Models
{
   public class Cart
   {
        public int ProductId { get; set; }
        public int Quantity { get; set; } = 1;

        public Product? Product { get; set; }


    }
 
}
