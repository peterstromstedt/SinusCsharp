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
        [RegularExpression(@"^[\w-.]+@([\w-]+.)+[\w-]{2,4}$"),Required]        
        public string Email { get; set; }

        // for swedish citizens this regex if for their phonenumber with +46
        // DO NOT FORGET TO CHECK THE REGEX YOU COPY PASTE... if they added spaces or not to the code
        [RegularExpression(@"^([+]46)\s(7[0236])\s(\d{4})\s(\d{3})$"), Required]
        // ^(?:(?:+|00)46|0)[\s-]?[1-9]\d{1,3}[\s-]?\d{4,8}$
        // ^(([+]46)\s*(7)|07)[02369]\s*(\d{4})\s*(\d{3})$
        public string PhoneNumber { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        // for swedish citizens this regex if for their postal service
        [RegularExpression(@"^[0-9]{3}\s[0-9]{2}$"), Required]
        public string Zip { get; set; }        
        public List<Order>? Orders { get; set; }
    }
}
