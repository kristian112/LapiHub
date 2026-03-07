using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lapihub.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = null!;

        public ApplicationUser? User { get; set; }

        [Required(ErrorMessage = "Адресът е задължителен.")]
        [StringLength(250, ErrorMessage = "Адресът не може да бъде по-дълъг от 250 символа.")]
        public string Address { get; set; } = null!;

        [Required(ErrorMessage = "Телефонът е задължителен.")]
        [Phone(ErrorMessage = "Моля, въведете валиден телефонен номер.")]
        public string PhoneNumber { get; set; } = null!;

        [Required(ErrorMessage = "Методът на плащане е задължителен.")]
        public PaymentMethod PaymentMethod { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Нова;

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}