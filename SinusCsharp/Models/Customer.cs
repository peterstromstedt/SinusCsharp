using System.ComponentModel.DataAnnotations;

namespace SinusCsharp.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required, StringLength(50, MinimumLength = 3)]        
        public string FirstName { get; set; }

        [Required, StringLength(50, MinimumLength = 3)]
        public string LastName { get; set; }

        // regex for checking correct email format
        [RegularExpression(@"^[\w-.]+@([\w -]+.)+[\w-]{2,4}$"),Required]        
        public string Email { get; set; }

        // for swedish citizens this regex if for their phonenumber with +46
        [RegularExpression(@"^([+]46)\s(7[0236])\s(\d{ 4})\s* (\d{3})$"), Required]        
        public string PhoneNumber { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        // for swedish citizens this regex if for their postal service
        [RegularExpression(@"^[0-9]{3}\s?[0 - 9]{2}$"), Required]
        public int Zip { get; set; }        
        public List<Order>? Orders { get; set; }
    }
}
