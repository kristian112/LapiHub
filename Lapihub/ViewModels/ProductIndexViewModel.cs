using Lapihub.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lapihub.ViewModels
{
    public class ProductIndexViewModel
    {
        public IEnumerable<Product> Products { get; set; } = new List<Product>();

        public string? SearchTerm { get; set; }

        public int? CategoryId { get; set; }

        public string? Brand { get; set; }

        public decimal? MinPrice { get; set; }

        public decimal? MaxPrice { get; set; }

        public string? SortBy { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; } = new List<SelectListItem>();

        public IEnumerable<string> Brands { get; set; } = new List<string>();

        public IEnumerable<BrandCountViewModel> BrandCounts { get; set; } = new List<BrandCountViewModel>();
    }
}