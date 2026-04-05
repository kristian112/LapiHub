using Lapihub.Data;
using Lapihub.Models;
using Lapihub.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lapihub.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories
                .Include(c => c.Products)
                .OrderBy(c => c.Name)
                .Select(c => new AdminCategoryDetailsViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    ProductsCount = c.Products.Count
                })
                .ToListAsync();

            return View(categories);
        }

        public async Task<IActionResult> Details(int id)
        {
            var category = await _context.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            var model = new AdminCategoryDetailsViewModel
            {
                Id = category.Id,
                Name = category.Name,
                ProductsCount = category.Products.Count
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new AdminCategoryFormViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdminCategoryFormViewModel model)
        {
            if (await _context.Categories.AnyAsync(c => c.Name == model.Name))
            {
                ModelState.AddModelError(nameof(model.Name), "Вече съществува категория с това име.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var category = new Category
            {
                Name = model.Name
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Категорията беше добавена успешно.";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            var model = new AdminCategoryFormViewModel
            {
                Id = category.Id,
                Name = category.Name
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AdminCategoryFormViewModel model)
        {
            var category = await _context.Categories.FindAsync(model.Id);

            if (category == null)
            {
                return NotFound();
            }

            if (await _context.Categories.AnyAsync(c => c.Id != model.Id && c.Name == model.Name))
            {
                ModelState.AddModelError(nameof(model.Name), "Вече съществува категория с това име.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            category.Name = model.Name;

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Категорията беше редактирана успешно.";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            var model = new AdminCategoryDetailsViewModel
            {
                Id = category.Id,
                Name = category.Name,
                ProductsCount = category.Products.Count
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            if (category.Products.Any())
            {
                TempData["ErrorMessage"] = "Категорията не може да бъде изтрита, защото има свързани продукти.";
                return RedirectToAction(nameof(Index));
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Категорията беше изтрита успешно.";

            return RedirectToAction(nameof(Index));
        }
    }
}