using Microsoft.AspNetCore.Mvc;
using SinusCsharp.Models;
using System.Diagnostics;
using System.Text.Json;

namespace SinusCsharp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (!HttpContext.Request.Cookies.ContainsKey("Cart"))
            {
                List<Cart> cart = new();
                CookieOptions cookieCart = new();
                cookieCart.Expires = DateTime.UtcNow.AddDays(5);
                cookieCart.Path = "/"; //Also defaultvalue. Will be accessible "everywhere"

                // https://code-maze.com/csharp-object-into-json-string-dotnet/
                var jsonString = JsonSerializer.Serialize(cart);

                Response.Cookies.Append("Cart", jsonString, cookieCart);
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}