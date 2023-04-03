using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SinusCsharp.Data;
using SinusCsharp.Models;

namespace SinusCsharp.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Order.Include(o => o.Customer);
            return View(await applicationDbContext.ToListAsync());
        }


        // GET: Orders/Create
        public async Task<IActionResult> Create()
        {
            //var id = Request.Cookies["Customer"];
            int customerId = int.Parse(Request.Cookies["Customer"]);
            if(customerId != 0)
            {
                Order order = new() {CustomerId = customerId, OrderDate = DateTime.Now, Shipped = true};
                _context.Add(order);
                await _context.SaveChangesAsync();
                int orderId = order.OrderId; //Get last inserted Id
                TempData["OrderId"] = orderId;
                return RedirectToAction("Create", "OrderDetails");
            }
            return View("Error");
        }


        // GET: Orders/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }
            var order = await _context.OrderDetail
                .Include(o => o.Order)
                .Include(o => o.Product)
                .Where(m => m.OrderId == id).ToListAsync();
                
            if (order == null)
            {
                return NotFound();
            }
            ViewBag.OrderId = id;

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Order == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Order'  is null.");
            }
            var orderDetails = await _context.OrderDetail.Where(od => od.OrderId == id).ToListAsync();
            if(orderDetails != null)
            {
                foreach(var row in orderDetails)
                {
                    _context.OrderDetail.Remove(row);
                }    
            }

            var order = await _context.Order.FindAsync(id);
            if (order != null)
            {
                _context.Order.Remove(order);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
          return (_context.Order?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }

    }
}
