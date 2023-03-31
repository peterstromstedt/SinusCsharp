﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SinusCsharp.Models
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailId { get; set; } 
        
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]        
        public Product? Product { get; set; }

        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order? Order { get; set; }

        public int Quantity { get; set; }
    }
}
