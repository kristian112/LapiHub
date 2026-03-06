using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lapihub.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Името на продукта е задължително.")]
        [StringLength(150, ErrorMessage = "Името на продукта не може да бъде по-дълго от 150 символа.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Марката е задължителна.")]
        [StringLength(100, ErrorMessage = "Марката не може да бъде по-дълга от 100 символа.")]
        public string Brand { get; set; } = null!;

        [Required(ErrorMessage = "Категорията е задължителна.")]
        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        [Required(ErrorMessage = "Цената е задължителна.")]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, 100000, ErrorMessage = "Цената трябва да бъде между 0.01 и 100000.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Описанието е задължително.")]
        [StringLength(2000, ErrorMessage = "Описанието не може да бъде по-дълго от 2000 символа.")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "Снимката е задължителна.")]
        [Display(Name = "Снимка")]
        public string ImageUrl { get; set; } = null!;

        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}