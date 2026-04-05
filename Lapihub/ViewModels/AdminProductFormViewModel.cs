using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lapihub.ViewModels
{
    public class AdminProductFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Името на продукта е задължително.")]
        [StringLength(150, ErrorMessage = "Името на продукта не може да бъде по-дълго от 150 символа.")]
        [Display(Name = "Име")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Марката е задължителна.")]
        [StringLength(100, ErrorMessage = "Марката не може да бъде по-дълга от 100 символа.")]
        [Display(Name = "Марка")]
        public string Brand { get; set; } = null!;

        [Required(ErrorMessage = "Категорията е задължителна.")]
        [Display(Name = "Категория")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Цената е задължителна.")]
        [Range(0.01, 100000, ErrorMessage = "Цената трябва да бъде между 0.01 и 100000.")]
        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Описанието е задължително.")]
        [StringLength(2000, ErrorMessage = "Описанието не може да бъде по-дълго от 2000 символа.")]
        [Display(Name = "Описание")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "Снимката е задължителна.")]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; } = null!;

        public List<SelectListItem> Categories { get; set; } = new();
    }
}