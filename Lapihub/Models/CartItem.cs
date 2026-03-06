using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lapihub.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = null!;

        public ApplicationUser? User { get; set; }

        [Required]
        public int ProductId { get; set; }

        public Product? Product { get; set; }

        [Range(1, 10, ErrorMessage = "Количество трябва да бъде между 1 и 10.")]
        public int Quantity { get; set; }

        [NotMapped]
        public decimal TotalPrice => (Product?.Price ?? 0) * Quantity;
    }
}