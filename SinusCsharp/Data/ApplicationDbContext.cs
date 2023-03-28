using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SinusCsharp.Models;

namespace SinusCsharp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<SinusCsharp.Models.Product> Product { get; set; } = default!;
        public DbSet<SinusCsharp.Models.Customer> Customer { get; set; } = default!;
        public DbSet<SinusCsharp.Models.Order> Order { get; set; } = default!;
        public DbSet<SinusCsharp.Models.OrderDetail> OrderDetail { get; set; } = default!;
    }
}