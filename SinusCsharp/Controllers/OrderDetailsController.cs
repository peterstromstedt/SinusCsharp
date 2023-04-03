using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SinusCsharp.Data;
using SinusCsharp.Models;

namespace SinusCsharp.Controllers
{
    public class OrderDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OrderDetails
        public async Task<IActionResult> Index()

        {
            var applicationDbContext = _context.OrderDetail.Include(o => o.Order).Include(o => o.Product);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: OrderDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (TempData["Orderid"] != null)
            {
                id = (int)TempData["Orderid"];
            }
                
            if (id == null || _context.OrderDetail == null)
            {
                return NotFound();
            }
               
            List<OrderDetail> orderDetails = await _context.OrderDetail
                .Include(o => o.Order)
                .Include(o => o.Product)
                .Where(m => m.OrderId == id).ToListAsync();
            if (orderDetails == null)
            {
                return NotFound();
            }
            ViewBag.OrderId = id;

            return View(orderDetails);
        }

        // GET: OrderDetails/Create
        public async Task<IActionResult> Create()
        {
            List<Cart> cartList = GetCartListFromCookie();
            var orderId = TempData["OrderId"];
            List<OrderDetail> odList = new();

            foreach(var item in cartList)
            {
                OrderDetail od = new()
                {
                    OrderId = (int)orderId,
                    ProductId = item.ProductId,
                    Quantity= item.Quantity
                };
                odList.Add(od);
            }

            _context.OrderDetail.AddRange(odList);
            await _context.SaveChangesAsync();

            //Clear cookies
            Response.Cookies.Delete("Cart");
            Response.Cookies.Delete("Customer");

            return RedirectToAction("Confirmation","Carts");
        }



        // GET: OrderDetails/Edit/5
        // Needs ta have variabel id to get asp-route-id from a-tag
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OrderDetail == null)
            {
                return NotFound();
            }

            var orderDetails = await _context.OrderDetail.Where(p => p.OrderDetailId == id).FirstOrDefaultAsync();

            if (orderDetails == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = orderDetails.OrderId;
            return View(orderDetails);
        }

        // POST: OrderDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderDetailId,ProductId,OrderId,Quantity")] OrderDetail orderDetail)
        {
            if (id != orderDetail.OrderDetailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderDetailExists(orderDetail.OrderDetailId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["Orderid"] = orderDetail.OrderId;
                return RedirectToAction("Details");
            }
            return View(orderDetail);
        }

        // GET: OrderDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OrderDetail == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderDetail
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.OrderDetailId == id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            return View(orderDetail);
        }

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OrderDetail == null)
            {
                return Problem("Entity set 'ApplicationDbContext.OrderDetail'  is null.");
            }
            var orderDetail = await _context.OrderDetail.FindAsync(id);
            if (orderDetail != null)
            {
                _context.OrderDetail.Remove(orderDetail);
            }
            
            await _context.SaveChangesAsync();
            TempData["OrderId"] = orderDetail.OrderId;
            return RedirectToAction("Details");
        }

        private bool OrderDetailExists(int id)
        {
          return (_context.OrderDetail?.Any(e => e.OrderDetailId == id)).GetValueOrDefault();
        }

        private List<Cart> GetCartListFromCookie()
        {
            //Unserialize cartList from Cookie
            var json = Request.Cookies["Cart"];
            List<Cart>? cartList = JsonSerializer.Deserialize<List<Cart>>(json);
            return cartList;
        }
    }
}
