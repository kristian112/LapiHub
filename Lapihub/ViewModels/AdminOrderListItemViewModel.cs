namespace Lapihub.ViewModels
{
    public class AdminOrderListItemViewModel
    {
        public int Id { get; set; }

        public string CustomerName { get; set; } = null!;

        public string CustomerEmail { get; set; } = null!;

        public DateTime CreatedOn { get; set; }

        public string Status { get; set; } = null!;

        public decimal TotalAmount { get; set; }

        public int ItemsCount { get; set; }
    }
}