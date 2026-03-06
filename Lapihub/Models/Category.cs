using System.ComponentModel.DataAnnotations;

namespace Lapihub.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Името на категорията е задължително.")]
        [StringLength(100, ErrorMessage = "Името на категорията не може да бъде по-дълго от 100 символа.")]
        public string Name { get; set; } = null!;

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}