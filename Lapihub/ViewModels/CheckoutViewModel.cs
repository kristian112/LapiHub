using System.ComponentModel.DataAnnotations;
using Lapihub.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lapihub.ViewModels
{
    public class CheckoutViewModel
    {
        [Required(ErrorMessage = "Полето Адрес е задължително.")]
        [StringLength(250, ErrorMessage = "Адресът не може да бъде по-дълъг от 250 символа.")]
        [Display(Name = "Адрес за доставка")]
        public string Address { get; set; } = null!;

        [Required(ErrorMessage = "Полето Телефон е задължително.")]
        [Phone(ErrorMessage = "Моля, въведете валиден телефонен номер.")]
        [Display(Name = "Телефон")]
        public string PhoneNumber { get; set; } = null!;

        [Required(ErrorMessage = "Полето Метод на плащане е задължително.")]
        [Display(Name = "Метод на плащане")]
        public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.НаложенПлатеж;

        public List<CartItemViewModel> CartItems { get; set; } = new();

        public List<SelectListItem> PaymentMethods { get; set; } = new();

        public decimal TotalAmount => CartItems.Sum(i => i.LineTotal);
    }
}