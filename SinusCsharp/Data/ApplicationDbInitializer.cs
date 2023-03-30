
using SinusCsharp.Models;

namespace SinusCsharp.Data
{
    public class ApplicationDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();

                //Product
                if (!context.Product.Any())
                {
                    context.Product.AddRange(new List<Product>()
                    {
                        new Product()
                        {
                            Title = "Hoodie",
                            ImageURL = ".\\images\\hoodie-ash.jpg",
                            Color ="Grey",
                            Price = 40,
                            Description="Grey hoodie",
                        },
                        new Product()
                        {
                            Title = "Hoodie",
                            ImageURL = ".\\images\\hoodie-green.jpg",
                            Color ="Green",
                            Price = 40,
                            Description="Green hoodie",
                        },
                        new Product()
                        {
                            Title = "Cap",
                            ImageURL = ".\\images\\sinus-cap-red.jpg",
                            Color ="Red",
                            Price = 5,
                            Description="Red cap",
                        },
                        new Product()
                        {
                            Title = "T-shirt",
                            ImageURL = ".\\images\\sinus-tshirt-blue.jpg",
                            Color ="Blue",
                            Price = 20,
                            Description="Blue T-shirt",
                        },
                        new Product()
                        {
                            Title = "T-shirt",
                            ImageURL = ".\\images\\sinus-tshirt-grey.jpg",
                            Color ="Grey",
                            Price = 20,
                            Description="Grey T-shirt",
                        },
                        new Product()
                        {
                            Title = "T-shirt",
                            ImageURL = ".\\images\\sinus-tshirt-pink.jpg",
                            Color ="Pink",
                            Price = 20,
                            Description="Pink T-shirt",
                        },
                        new Product()
                        {
                            Title = "T-shirt",
                            ImageURL = ".\\images\\sinus-tshirt-purple.jpg",
                            Color ="Purple",
                            Price = 20,
                            Description="Purple T-shirt",
                        },
                        new Product()
                        {
                            Title = "T-shirt",
                            ImageURL = ".\\images\\sinus-tshirt-yellow.jpg",
                            Color ="Yellow",
                            Price = 20,
                            Description="Yellow T-shirt",
                        },
                        new Product()
                        {
                            Title = "Wheel",
                            ImageURL = ".\\images\\sinus-wheel-rocket.jpg",
                            Color = "Red",
                            Price = 10,
                            Description = "Red wheel (rocket)",
                        },
                        new Product()
                        {
                            Title = "Wheel",
                            ImageURL = ".\\images\\sinus-wheel-wave.jpg",
                            Color = "Grey",
                            Price = 10,
                            Description = "Grey wheel (wave)",
                        },
                    });
                    context.SaveChanges();
                }
                //Customer
                if (!context.Customer.Any())
                {
                    context.Customer.AddRange(new List<Customer>()
                    {
                        new Customer()
                        {
                            FirstName="Jane",
                            LastName="Doe",
                            Email="jane.doe@mail.mail",
                            PhoneNumber="+46 70 1111 111",
                            Address="Xvägen",
                            City="Xstad",
                            Zip="111 11",
                        },
                        new Customer()
                        {
                            FirstName="John",
                            LastName="Doe",
                            Email="john.doe@mail.mail",
                            PhoneNumber="+46 70 2222 222",
                            Address="Ygatan",
                            City="Yby",
                            Zip="222 22",
                        },
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}
