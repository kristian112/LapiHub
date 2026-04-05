using Lapihub.Data;
using Lapihub.Models;
using Lapihub.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lapihub.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminUsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminUsersController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _context.Users
                .Include(u => u.Orders)
                .Include(u => u.CartItems)
                .OrderBy(u => u.FirstName)
                .ThenBy(u => u.LastName)
                .ToListAsync();

            var model = new List<AdminUserListItemViewModel>();

            foreach (var user in users)
            {
                model.Add(new AdminUserListItemViewModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email ?? string.Empty,
                    PhoneNumber = user.PhoneNumber,
                    OrdersCount = user.Orders.Count,
                    CartItemsCount = user.CartItems.Count,
                    IsAdmin = await _userManager.IsInRoleAsync(user, "Admin")
                });
            }

            return View(model);
        }

        public async Task<IActionResult> Details(string id)
        {
            var user = await _context.Users
                .Include(u => u.Orders)
                .Include(u => u.CartItems)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            var model = new AdminUserDetailsViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email ?? string.Empty,
                PhoneNumber = user.PhoneNumber,
                OrdersCount = user.Orders.Count,
                CartItemsCount = user.CartItems.Count,
                IsAdmin = await _userManager.IsInRoleAsync(user, "Admin")
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            var model = new AdminUserEditViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email ?? string.Empty,
                PhoneNumber = user.PhoneNumber ?? string.Empty
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AdminUserEditViewModel model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == model.Id);

            if (user == null)
            {
                return NotFound();
            }

            var emailExists = await _context.Users
                .AnyAsync(u => u.Id != model.Id && u.Email == model.Email);

            if (emailExists)
            {
                ModelState.AddModelError(nameof(model.Email), "Вече съществува потребител с този имейл.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.UserName = model.Email;
            user.NormalizedEmail = model.Email.ToUpper();
            user.NormalizedUserName = model.Email.ToUpper();
            user.PhoneNumber = model.PhoneNumber;

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Потребителят беше редактиран успешно.";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _context.Users
                .Include(u => u.Orders)
                .Include(u => u.CartItems)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            var model = new AdminUserDetailsViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email ?? string.Empty,
                PhoneNumber = user.PhoneNumber,
                OrdersCount = user.Orders.Count,
                CartItemsCount = user.CartItems.Count,
                IsAdmin = await _userManager.IsInRoleAsync(user, "Admin")
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var currentAdmin = await _userManager.GetUserAsync(User);
            var user = await _context.Users
                .Include(u => u.Orders)
                .Include(u => u.CartItems)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            if (currentAdmin != null && currentAdmin.Id == user.Id)
            {
                TempData["ErrorMessage"] = "Не можете да изтриете собствения си акаунт.";
                return RedirectToAction(nameof(Index));
            }

            if (user.Orders.Any())
            {
                TempData["ErrorMessage"] = "Потребителят не може да бъде изтрит, защото има направени поръчки.";
                return RedirectToAction(nameof(Index));
            }

            if (user.CartItems.Any())
            {
                _context.CartItems.RemoveRange(user.CartItems);
            }

            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Any())
            {
                await _userManager.RemoveFromRolesAsync(user, roles);
            }

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                TempData["ErrorMessage"] = "Възникна проблем при изтриването на потребителя.";
                return RedirectToAction(nameof(Index));
            }

            TempData["SuccessMessage"] = "Потребителят беше изтрит успешно.";

            return RedirectToAction(nameof(Index));
        }
    }
}