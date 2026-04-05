using System.ComponentModel.DataAnnotations;

namespace Lapihub.ViewModels
{
    public class AdminCategoryFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Името на категорията е задължително.")]
        [StringLength(100, ErrorMessage = "Името на категорията не може да бъде по-дълго от 100 символа.")]
        [Display(Name = "Име на категория")]
        public string Name { get; set; } = null!;
    }
}