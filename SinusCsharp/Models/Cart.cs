namespace SinusCsharp.Models
{
    public class Cart
    {
        public int CartId { get; set; }        
        public int ProductId { get; set; }
        public int Quantity { get; set; } = 1;
    }
}
