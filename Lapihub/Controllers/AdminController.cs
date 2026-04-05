using Lapihub.Data;
using Lapihub.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lapihub.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var model = new AdminDashboardViewModel
            {
                ProductsCount = await _context.Products.CountAsync(),
                CategoriesCount = await _context.Categories.CountAsync(),
                OrdersCount = await _context.Orders.CountAsync(),
                UsersCount = await _context.Users.CountAsync()
            };

            return View(model);
        }
    }
}