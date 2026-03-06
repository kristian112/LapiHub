using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Lapihub.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "Полето Име е задължително.")]
        [StringLength(50, ErrorMessage = "Името не може да бъде по-дълго от 50 символа.")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Полето Фамилия е задължително.")]
        [StringLength(50, ErrorMessage = "Фамилията не може да бъде по-дълга от 50 символа.")]
        public string LastName { get; set; } = null!;

        public string FullName => $"{FirstName} {LastName}";

        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}