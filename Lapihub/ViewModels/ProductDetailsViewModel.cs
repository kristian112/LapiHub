using Lapihub.Models;

namespace Lapihub.ViewModels
{
    public class ProductDetailsViewModel
    {
        public Product Product { get; set; } = null!;

        public IEnumerable<Product> RelatedProducts { get; set; } = new List<Product>();
    }
}