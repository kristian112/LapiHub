namespace Lapihub.ViewModels
{
    public class AdminOrderDetailsViewModel
    {
        public int Id { get; set; }

        public string CustomerName { get; set; } = null!;

        public string CustomerEmail { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string PaymentMethod { get; set; } = null!;

        public string Status { get; set; } = null!;

        public DateTime CreatedOn { get; set; }

        public decimal TotalAmount { get; set; }

        public List<OrderDetailsItemViewModel> Items { get; set; } = new();
    }
}