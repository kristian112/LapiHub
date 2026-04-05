namespace Lapihub.ViewModels
{
    public class MyOrderListItemViewModel
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Status { get; set; } = null!;

        public decimal TotalAmount { get; set; }

        public int ItemsCount { get; set; }
    }
}