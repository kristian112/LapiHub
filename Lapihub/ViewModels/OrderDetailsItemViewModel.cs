namespace Lapihub.ViewModels
{
    public class OrderDetailsItemViewModel
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public string Brand { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal LineTotal => Price * Quantity;
    }
}