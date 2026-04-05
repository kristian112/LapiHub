using System.ComponentModel.DataAnnotations;

namespace Lapihub.ViewModels
{
    public class AdminUserEditViewModel
    {
        public string Id { get; set; } = null!;

        [Required(ErrorMessage = "Полето Име е задължително.")]
        [StringLength(50, ErrorMessage = "Името не може да бъде по-дълго от 50 символа.")]
        [Display(Name = "Име")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Полето Фамилия е задължително.")]
        [StringLength(50, ErrorMessage = "Фамилията не може да бъде по-дълга от 50 символа.")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "Полето Имейл е задължително.")]
        [EmailAddress(ErrorMessage = "Моля, въведете валиден имейл адрес.")]
        [Display(Name = "Имейл")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Полето Телефон е задължително.")]
        [Phone(ErrorMessage = "Моля, въведете валиден телефонен номер.")]
        [Display(Name = "Телефон")]
        public string PhoneNumber { get; set; } = null!;
    }
}