using Lapihub.Data;
using Lapihub.Models;
using Lapihub.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Lapihub.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrdersController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> MyOrders()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return Challenge();
            }

            var orders = await _context.Orders
                .Where(o => o.UserId == user.Id)
                .Include(o => o.OrderItems)
                .OrderByDescending(o => o.CreatedOn)
                .Select(o => new MyOrderListItemViewModel
                {
                    Id = o.Id,
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
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return Challenge();
            }

            var order = await _context.Orders
                .Where(o => o.Id == id && o.UserId == user.Id)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync();

            if (order == null)
            {
                return NotFound();
            }

            var model = new OrderDetailsViewModel
            {
                Id = order.Id,
                CreatedOn = order.CreatedOn,
                Status = order.Status.ToString(),
                Address = order.Address,
                PhoneNumber = order.PhoneNumber,
                PaymentMethod = order.PaymentMethod == PaymentMethod.НаложенПлатеж
    ? "Наложен платеж"
    : order.PaymentMethod.ToString(),
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
        public async Task<IActionResult> Checkout()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return Challenge();
            }

            var cartItems = await GetUserCartItemsAsync(user.Id);

            if (!cartItems.Any())
            {
                TempData["ErrorMessage"] = "Количката е празна. Добавете продукти преди да продължите към поръчка.";
                return RedirectToAction("Index", "Cart");
            }

            var model = new CheckoutViewModel
            {
                Address = string.Empty,
                PhoneNumber = user.PhoneNumber ?? string.Empty,
                PaymentMethod = PaymentMethod.НаложенПлатеж,
                CartItems = cartItems,
                PaymentMethods = GetPaymentMethods()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(CheckoutViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return Challenge();
            }

            var cartItems = await _context.CartItems
                .Where(ci => ci.UserId == user.Id)
                .Include(ci => ci.Product)
                .ToListAsync();

            if (!cartItems.Any())
            {
                TempData["ErrorMessage"] = "Количката е празна. Добавете продукти преди да продължите към поръчка.";
                return RedirectToAction("Index", "Cart");
            }

            model.CartItems = cartItems
                .Select(ci => new CartItemViewModel
                {
                    CartItemId = ci.Id,
                    ProductId = ci.ProductId,
                    ProductName = ci.Product!.Name,
                    Brand = ci.Product.Brand,
                    ImageUrl = ci.Product.ImageUrl,
                    Price = ci.Product.Price,
                    Quantity = ci.Quantity
                })
                .ToList();

            model.PaymentMethods = GetPaymentMethods();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var order = new Order
            {
                UserId = user.Id,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
                PaymentMethod = model.PaymentMethod,
                Status = OrderStatus.Нова,
                CreatedOn = DateTime.Now,
                TotalAmount = cartItems.Sum(ci => ci.Product!.Price * ci.Quantity)
            };

            foreach (var cartItem in cartItems)
            {
                order.OrderItems.Add(new OrderItem
                {
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity,
                    Price = cartItem.Product!.Price
                });
            }

            _context.Orders.Add(order);
            _context.CartItems.RemoveRange(cartItems);

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Поръчката беше създадена успешно.";

            return RedirectToAction(nameof(MyOrders));
        }

        private async Task<List<CartItemViewModel>> GetUserCartItemsAsync(string userId)
        {
            return await _context.CartItems
                .Where(ci => ci.UserId == userId)
                .Include(ci => ci.Product)
                .OrderBy(ci => ci.Product!.Name)
                .Select(ci => new CartItemViewModel
                {
                    CartItemId = ci.Id,
                    ProductId = ci.ProductId,
                    ProductName = ci.Product!.Name,
                    Brand = ci.Product.Brand,
                    ImageUrl = ci.Product.ImageUrl,
                    Price = ci.Product.Price,
                    Quantity = ci.Quantity
                })
                .ToListAsync();
        }

        private List<SelectListItem> GetPaymentMethods()
        {
            return new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value = PaymentMethod.НаложенПлатеж.ToString(),
                    Text = "Наложен платеж"
                }
            };
        }
    }
}