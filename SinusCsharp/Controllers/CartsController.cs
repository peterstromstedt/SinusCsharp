
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SinusCsharp.Data;
using SinusCsharp.Data.Services;
using SinusCsharp.Models;


namespace SinusCsharp.Controllers
{
    public class CartsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICartService _cartService;

        public CartsController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor,
            ICartService cartService)
        {
            _cartService = cartService;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: Carts
        public IActionResult Index()
        {
            List<Cart>? cartList = new();
            if(HttpContext.Request.Cookies.ContainsKey("Cart")) 
            {
                var json = Request.Cookies["Cart"];
                cartList = JsonSerializer.Deserialize<List<Cart>>(json);
            } 

            return View(cartList);
        }

        // GET: Carts/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            List<Cart> cartList = GetCartListFromCookie();
            var item = cartList.FirstOrDefault(i => i.ProductId == id);
            
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        // POST: Carts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ProductId,Quantity")] Cart cart)
        {
            if (id != cart.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                List<Cart> cartList = GetCartListFromCookie();
                cartList = _cartService.UpdateQuantityOfAProduct(cartList, cart);
                AddCartListToCookie(cartList);
  
                return RedirectToAction(nameof(Index));
            }
            return View(cart);
        }

        // GET: Carts/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            List<Cart> cartList = GetCartListFromCookie();

            var cart = cartList.FirstOrDefault(m => m.ProductId == id);

            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {

            var cartList = GetCartListFromCookie();

            if (cartList != null)
            {
                cartList.RemoveAll(i => i.ProductId == id);
            }
            AddCartListToCookie(cartList);

            return RedirectToAction(nameof(Index));
        }

        //Get
        public IActionResult Buy(int id)
        {

            Product? product = _context.Product.FirstOrDefault(p => p.ProductId == id);

            Cart cartobject = new() { ProductId = id, Quantity = 1, Product = product };

            List<Cart>? cartList = GetCartListFromCookie();

            cartList = _cartService.AddProductToCart(cartList, cartobject);

            AddCartListToCookie(cartList);

            return RedirectToAction(nameof(Index));
        }
        //
        public IActionResult Pay(int customerId)
        {
            if (!HttpContext.Request.Cookies.ContainsKey("Customer"))
            {
                CookieOptions cookieCustomer = new CookieOptions();
                cookieCustomer.Expires = DateTime.UtcNow.AddMinutes(5);
                cookieCustomer.Path = "/"; //Also defaultvalue. Will be accessible "everywhere"

                // https://code-maze.com/csharp-object-into-json-string-dotnet/
                //var jsonString = JsonSerializer.Serialize(customerId);

                Response.Cookies.Append("Customer", customerId.ToString(), cookieCustomer);
           
            }
            List<Cart> cartList = GetCartListFromCookie();
            return View(cartList);
        }
        public IActionResult Confirmation()
        {
            // Get and send Customer,Order and Productlist to view
            // Partialview only return the Confirmation view without the layout
            return PartialView();
        }


        public List<Cart> GetCartListFromCookie()
        {
            //Unserialize cartList from Cookie
            var json = Request.Cookies["Cart"];
            List<Cart>? cartList = JsonSerializer.Deserialize<List<Cart>>(json);
            return cartList;
        }
        private void AddCartListToCookie(List<Cart> cartList)
        {
            //Serialize new cartList
            var jsonString = JsonSerializer.Serialize(cartList);
            Response.Cookies.Append("Cart", jsonString);
        }



        // Not needed but nice example of try-catch specifik sqlException
        /*public async Task<IActionResult> Buy2(int id)
        {

            if (ModelState.IsValid)
            {
                Cart cart = new Cart() { ProductId = id, Quantity = 1};
                try
                {
                    _context.Add(cart);
                    await _context.SaveChangesAsync();  
                }
                catch (DbUpdateException ex) 
                {
                    if (ex.InnerException is SqlException sqlException)
                    {
                        // Check if the error code indicates a unique key constraint violation
                        if (sqlException.Number == 2627 || sqlException.Number == 2601)
                        {
                            //Only update the specifik field qty
                            var cartItem = _context.Cart.SingleOrDefault(x => x.ProductId == id);
                            int qty = cartItem.Quantity +1;
         
                            _context.Cart.Where(x => x.ProductId == id)
                                .ExecuteUpdate(q => q.SetProperty(p => p.Quantity, qty));
                        }
                        else
                        {
                            // Handle other SQL exceptions
                            // For example:
                            Console.WriteLine("An SQL exception occurred with error code " + sqlException.Number);
                        }
                    }

                }
                
            }
            return RedirectToAction("Index", "Products");
        } */
    }
}
