using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SinusCsharp.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public int CustomerId { get; set; }        
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        // setting 2 default values with the = _____ ...
        public bool Shipped { get; set; } = false;
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public List<OrderDetail>? Details { get; set; }
    }
}
