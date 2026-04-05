namespace Lapihub.ViewModels
{
    public class OrderDetailsViewModel
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Status { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string PaymentMethod { get; set; } = null!;

        public decimal TotalAmount { get; set; }

        public List<OrderDetailsItemViewModel> Items { get; set; } = new();
    }
}