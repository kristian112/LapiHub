using Lapihub.Data;
using Lapihub.Models;
using Lapihub.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lapihub.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminOrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminOrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .OrderByDescending(o => o.CreatedOn)
                .Select(o => new AdminOrderListItemViewModel
                {
                    Id = o.Id,
                    CustomerName = o.User!.FirstName + " " + o.User.LastName,
                    CustomerEmail = o.User.Email ?? string.Empty,
                    CreatedOn = o.CreatedOn,
                    Status = o.Status.ToString(),
                    TotalAmount = o.TotalAmount,
                    ItemsCount = o.OrderItems.Count
                })
                .ToListAsync();

            return View(orders);
        }

        public async Task<IActionResult> Details(int id)
        {
            var order = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            var model = new AdminOrderDetailsViewModel
            {
                Id = order.Id,
                CustomerName = order.User!.FirstName + " " + order.User.LastName,
                CustomerEmail = order.User.Email ?? string.Empty,
                PhoneNumber = order.PhoneNumber,
                Address = order.Address,
                PaymentMethod = order.PaymentMethod == PaymentMethod.НаложенПлатеж
                    ? "Наложен платеж"
                    : order.PaymentMethod.ToString(),
                Status = order.Status.ToString(),
                CreatedOn = order.CreatedOn,
                TotalAmount = order.TotalAmount,
                Items = order.OrderItems
                    .Select(oi => new OrderDetailsItemViewModel
                    {
                        ProductId = oi.ProductId,
                        ProductName = oi.Product!.Name,
                        Brand = oi.Product.Brand,
                        ImageUrl = oi.Product.ImageUrl,
                        Quantity = oi.Quantity,
                        Price = oi.Price
                    })
                    .ToList()
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            var model = new AdminOrderListItemViewModel
            {
                Id = order.Id,
                CustomerName = order.User!.FirstName + " " + order.User.LastName,
                CustomerEmail = order.User.Email ?? string.Empty,
                CreatedOn = order.CreatedOn,
                Status = order.Status.ToString(),
                TotalAmount = order.TotalAmount,
                ItemsCount = order.OrderItems.Count
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            _context.OrderItems.RemoveRange(order.OrderItems);
            _context.Orders.Remove(order);

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Поръчката беше изтрита успешно.";

            return RedirectToAction(nameof(Index));
        }
    }
}