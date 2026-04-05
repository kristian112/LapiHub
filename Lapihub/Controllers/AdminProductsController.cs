using Lapihub.Data;
using Lapihub.Models;
using Lapihub.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Lapihub.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .OrderBy(p => p.Name)
                .Select(p => new AdminProductDetailsViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Brand = p.Brand,
                    CategoryName = p.Category!.Name,
                    Price = p.Price,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl
                })
                .ToListAsync();

            return View(products);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            var model = new AdminProductDetailsViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Brand = product.Brand,
                CategoryName = product.Category!.Name,
                Price = product.Price,
                Description = product.Description,
                ImageUrl = product.ImageUrl
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new AdminProductFormViewModel
            {
                Categories = await GetCategorySelectListAsync()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdminProductFormViewModel model)
        {
            model.Categories = await GetCategorySelectListAsync();

            if (!await _context.Categories.AnyAsync(c => c.Id == model.CategoryId))
            {
                ModelState.AddModelError(nameof(model.CategoryId), "Моля, изберете валидна категория.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var product = new Product
            {
                Name = model.Name,
                Brand = model.Brand,
                CategoryId = model.CategoryId,
                Price = model.Price,
                Description = model.Description,
                ImageUrl = model.ImageUrl
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Продуктът беше добавен успешно.";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            var model = new AdminProductFormViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Brand = product.Brand,
                CategoryId = product.CategoryId,
                Price = product.Price,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Categories = await GetCategorySelectListAsync()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AdminProductFormViewModel model)
        {
            var product = await _context.Products.FindAsync(model.Id);

            if (product == null)
            {
                return NotFound();
            }

            model.Categories = await GetCategorySelectListAsync();

            if (!await _context.Categories.AnyAsync(c => c.Id == model.CategoryId))
            {
                ModelState.AddModelError(nameof(model.CategoryId), "Моля, изберете валидна категория.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            product.Name = model.Name;
            product.Brand = model.Brand;
            product.CategoryId = model.CategoryId;
            product.Price = model.Price;
            product.Description = model.Description;
            product.ImageUrl = model.ImageUrl;

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Продуктът беше редактиран успешно.";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            var model = new AdminProductDetailsViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Brand = product.Brand,
                CategoryName = product.Category!.Name,
                Price = product.Price,
                Description = product.Description,
                ImageUrl = product.ImageUrl
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            try
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Продуктът беше изтрит успешно.";
            }
            catch
            {
                TempData["ErrorMessage"] = "Продуктът не може да бъде изтрит, защото участва в количка или поръчка.";
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<List<SelectListItem>> GetCategorySelectListAsync()
        {
            return await _context.Categories
                .OrderBy(c => c.Name)
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                })
                .ToListAsync();
        }
    }
}